using UnityEngine;
using DG.Tweening;
using System;

namespace Game.Animation
{
    public class BounceAnimation : BaseAnimation
    {
        [SerializeField]
        private Vector2 _startingScale = Vector2.zero;

        [SerializeField]
        private Vector2 _bounceScale = new Vector2(1.2f, 1.2f);

        [SerializeField]
        private float _bounceInDuration = 0.3f;

        [SerializeField]
        private float _bounceOutDuration = 0.25f;

        public override void PlayAnimation(Action onAnimationEnded = null)
        {
            StopAnimation();

            var originalScale = transform.localScale;
            transform.localScale = _startingScale;

            _tweener = transform.DOScale(_bounceScale, _bounceInDuration).SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    transform.DOScale(originalScale, _bounceOutDuration).SetEase(Ease.InOutSine)
                        .OnComplete(() => onAnimationEnded?.Invoke());
                });
        }
    }
}