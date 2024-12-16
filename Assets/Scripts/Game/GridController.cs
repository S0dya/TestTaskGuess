using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GridController : MonoBehaviour
{
    [SerializeField]
    private GridLayoutGroup _gridLayoutGroup;

    [SerializeField]
    private Slot _slotPrefab;

    [SerializeField]
    private Transform _slotsParent;

    private List<Slot> _currentSlots = new List<Slot>();

    private int _rightCardI;

    public void StartLevel(CardData[] cards, int rows, int rightCardI, Action onGuessedRight, bool animate = false)
    {
        ClearSlots();

        _rightCardI = rightCardI;

        _gridLayoutGroup.constraintCount = rows;

        for (int i = 0; i < cards.Length; i++)
        {
            var slot = Instantiate(_slotPrefab, _slotsParent);
            _currentSlots.Add(slot);

            bool isRight = i == _rightCardI;
            Action guessAction = null;
            if (isRight)
            {
                guessAction = onGuessedRight;
            }
            else
            {
                guessAction = () => OnGuessedWrong(i);
            }

            slot.Init(cards[i].Sprite, guessAction, isRight, animate);

            if (animate)
            {
            }
        }
    }

    public Vector2 GetRightCardPosition()
    {
        return _currentSlots[_rightCardI].transform.position;
    }

    private void OnGuessedWrong(int i)
    {

    }

    private void ClearSlots()
    {
        foreach (var slot in _currentSlots)
        {
            Destroy(slot.gameObject);
        }
        _currentSlots.Clear();
    }

}
