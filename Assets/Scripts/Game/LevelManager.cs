using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private LevelData[] _levelDatas;

    [Inject]
    private GridController _gridController;

    [Inject]
    private UITaskDescriptionController _uiTaskDescriptionController;

    [Inject]
    private UIParticleEffects _uiParticleEffects;

    private int _levelIndex = 0;

    private List<string> _usedIdentifiers = new List<string>();

    private void Start()
    {
        StartLevel(_levelDatas[0]);
    }

    public void StartLevel(LevelData levelData)
    {
        int rows = levelData.Rows * (_levelIndex + 1);

        var cards = Helper.GetRandomElement(levelData.CardBundlesData).CardsData;
        Helper.Shuffle(cards);

        var rightCard = cards.First(x => !_usedIdentifiers.Contains(x.Identifier));
        _usedIdentifiers.Add(rightCard.Identifier);

        int rightCardI = Array.IndexOf(cards, rightCard);
        if (rightCardI > rows)
            cards[Helper.GetRandomNextInt(rows)] = rightCard;

        var resizedCards = cards.Take(rows).ToArray();

        _gridController.StartLevel(
            resizedCards,
            rows,
            rightCardI,
            OnGuessedRight,
            _levelIndex == 0);


    }

    public void OnGuessedRight()
    {
        _uiParticleEffects.PlayGuessedRightParticleSystem(_gridController.GetRightCardPosition());

        _levelIndex++;

        if (_levelIndex >= _levelDatas[0].LevelsAmount)
        {

        }
        StartLevel(_levelDatas[0]);
    }

}
