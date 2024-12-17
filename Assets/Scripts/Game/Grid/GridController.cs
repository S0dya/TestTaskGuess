using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Grid
{
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
            InitializeLevel(cards, rows, rightCardI, onGuessedRight, animate);
        }

        public void DisableSlots()
        {
            foreach (var slot in _currentSlots)
            {
                slot.DisableButton();
            }
        }

        public void ClearGrid()
        {
            ClearSlots();
        }

        public Vector2 GetRightCardPosition()
        {
            return _currentSlots[_rightCardI].transform.position;
        }

        private void InitializeLevel(CardData[] cards, int rows, int rightCardI, Action onGuessedRight, bool animate)
        {
            ClearSlots();

            _rightCardI = rightCardI;
            _gridLayoutGroup.constraintCount = rows;

            for (int i = 0; i < cards.Length; i++)
            {
                var slot = CreateSlot();
                bool isRight = i == _rightCardI;
                Action guessAction = isRight ? onGuessedRight : null;

                InitializeSlot(slot, cards[i], guessAction, isRight, animate);
            }
        }

        private Slot CreateSlot()
        {
            var slot = Instantiate(_slotPrefab, _slotsParent);
            _currentSlots.Add(slot);

            return slot;
        }

        private void InitializeSlot(Slot slot, CardData card, Action guessAction, bool isRight, bool animate)
        {
            slot.Init(card.CardVisual, guessAction, isRight, animate);
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
}
