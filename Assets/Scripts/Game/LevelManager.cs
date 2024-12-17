using Game.Data;
using Game.Grid;
using Game.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private float _guessedRightDelay = 0.4f;

        [SerializeField]
        private LevelData _levelData;

        [Inject]
        private GridController _gridController;

        [Inject]
        private UITaskDescriptionController _uiTaskDescriptionController;

        [Inject]
        private UIParticleEffects _uiParticleEffects;

        [Inject]
        private UILevelFinish _uiLevelFinish;

        private int _levelIndex = 0;
        private List<string> _usedIdentifiers = new List<string>();

        private void Start()
        {
            StartLevel(_levelData);
        }

        public void StartNewLevel()
        {
            StartLevel(_levelData);
        }

        public async void OnGuessedRight()
        {
            _gridController.DisableSlots();
            _uiParticleEffects.PlayGuessedRightParticleSystem(_gridController.GetRightCardPosition());

            await Helper.WaitInSeconds(_guessedRightDelay);

            _levelIndex++;

            if (_levelIndex >= _levelData.LevelsAmount)
            {
                _uiLevelFinish.OpenTab();
            }
            else
            {
                StartLevel(_levelData);
            }
        }

        public void ClearLevel()
        {
            _levelIndex = 0;
            _uiTaskDescriptionController.ClearDescription();
            _gridController.ClearGrid();
            _uiParticleEffects.ClearParticles();

            _usedIdentifiers.Clear();
        }

        private void StartLevel(LevelData levelData)
        {
            int elementsAmount = GetElementsAmount(levelData);
            var cards = GetShuffledCards(levelData);
            var unusedCards = FilterUnusedCards(cards);
            var rightCard = SelectRightCard(unusedCards);
            int rightCardI = PlaceRightCard(cards, rightCard, elementsAmount);
            var resizedCards = ResizeCards(cards, elementsAmount);

            bool animate = _levelIndex == 0;

            StartGridLevel(resizedCards, rightCardI, animate);
            SetTaskDescription(rightCard, animate);
        }

        private int GetElementsAmount(LevelData levelData)
        {
            return levelData.Rows * (_levelIndex + 1);
        }

        private CardData[] GetShuffledCards(LevelData levelData)
        {
            var cardsBundle = _levelIndex == 0 ? levelData.CardBundlesData[0] : Helper.GetRandomElement(levelData.CardBundlesData);
            var cards = cardsBundle.CardsData.ToArray();
            Helper.Shuffle(cards);

            return cards;
        }

        private CardData[] FilterUnusedCards(CardData[] cards)
        {
            return cards.Where(x => !_usedIdentifiers.Contains(x.Identifier)).ToArray();
        }

        private CardData SelectRightCard(CardData[] unusedCards)
        {
            var rightCard = Helper.GetRandomElement(unusedCards);
            _usedIdentifiers.Add(rightCard.Identifier);

            return rightCard;
        }

        private int PlaceRightCard(CardData[] cards, CardData rightCard, int elementsAmount)
        {
            int rightCardI = Array.IndexOf(cards, rightCard);

            if (rightCardI >= elementsAmount)
            {
                rightCardI = Helper.GetRandomNextInt(elementsAmount);
                cards[rightCardI] = rightCard;
            }

            return rightCardI;
        }

        private CardData[] ResizeCards(CardData[] cards, int elementsAmount)
        {
            return cards.Take(elementsAmount).ToArray();
        }

        private void StartGridLevel(CardData[] resizedCards, int rightCardI, bool animate)
        {
            _gridController.StartLevel(
                resizedCards,
                _levelData.Rows,
                rightCardI,
                OnGuessedRight,
                animate);
        }

        private void SetTaskDescription(CardData rightCard, bool animate)
        {
            _uiTaskDescriptionController.SetDescription(rightCard.Identifier, animate);
        }
    }
}
