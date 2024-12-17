using Game.Animation;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Grid
{
    public class Slot : MonoBehaviour
    {
        [SerializeField]
        private Image _cardImage;

        [SerializeField]
        private Button _guessButton;
    
        [SerializeField]
        private BaseAnimation _spawnBounceAnimation;
    
        [SerializeField]
        private BaseAnimation _bounceRightGuessAnimation;
    
        [SerializeField]
        private BaseAnimation _easeInBounceWrongAnimation;
    
        public void Init(CardVisual cardVisual, Action guessAction, bool isRight, bool animate)
        {
            _cardImage.sprite = cardVisual.Sprite;
            _cardImage.transform.rotation = Quaternion.Euler(0, 0, cardVisual.Rotates ? -90 : 0);

            _guessButton.onClick.AddListener(() => 
            { 
                guessAction?.Invoke(); 

                if (isRight)
                {
                    _bounceRightGuessAnimation.PlayAnimation();
                }
                else
                {
                    _easeInBounceWrongAnimation.PlayAnimation();
                }
            });

            if (animate)
            {
                _spawnBounceAnimation.PlayAnimation();
            }
        }

        public void DisableButton()
        {
            _cardImage.raycastTarget = false;
        }
    }
}
