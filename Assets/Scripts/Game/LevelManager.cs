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
            GetCardsData(levelData, out CardData[] cards, out int rightCardI, out string rightCardIdentifier);

            bool animate = _levelIndex == 0;

            _gridController.StartLevel(
                cards,
                _levelData.Rows,
                rightCardI,
                OnGuessedRight,
                animate);
            _uiTaskDescriptionController.SetDescription(rightCardIdentifier, animate);
        }

        private void GetCardsData(LevelData levelData,
            out CardData[] cards, out int rightCardI, out string rightCardIdentifier)
        {
            int elementsAmount = levelData.Rows * (_levelIndex + 1);
            
            cards = GetShuffledCards(levelData);
            var unusedCards = GetFilterUnusedCards(cards);

            var rightCard = Helper.GetRandomElement(unusedCards);
            rightCardIdentifier = rightCard.Identifier;
            _usedIdentifiers.Add(rightCardIdentifier);
            
            rightCardI = PlaceRightCard(cards, rightCard, elementsAmount);
            cards = cards.Take(elementsAmount).ToArray();
        }

        private CardData[] GetShuffledCards(LevelData levelData)
        {
            var cardsBundle = _levelIndex == 0 ? levelData.CardBundlesData[0] : Helper.GetRandomElement(levelData.CardBundlesData);
            var cards = cardsBundle.CardsData.ToArray();
            Helper.Shuffle(cards);

            return cards;
        }

        private CardData[] GetFilterUnusedCards(CardData[] cards)
        {
            return cards.Where(x => !_usedIdentifiers.Contains(x.Identifier)).ToArray();
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
    }
}
