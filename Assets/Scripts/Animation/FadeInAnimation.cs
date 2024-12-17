using DG.Tweening;
using System;
using UnityEngine;

namespace Game.Animation
{
    public class FadeInAnimation : BaseAnimation
    {
        [SerializeField]
        private float _finalAlpha = 1;

        [SerializeField]
        private float _fadeInDuration = 1;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        public override void StopAnimation()
        {
            base.StopAnimation();

            _canvasGroup.alpha = _finalAlpha;
            _canvasGroup.blocksRaycasts = true;
        }

        public override void PlayAnimation(Action onAnimationEnded = null)
        {
            base.StopAnimation();

            _canvasGroup.blocksRaycasts = true;

            _tweener = _canvasGroup.DOFade(_finalAlpha, _fadeInDuration).OnComplete(() =>
            {
                onAnimationEnded?.Invoke();
            });
        }
    }
}
