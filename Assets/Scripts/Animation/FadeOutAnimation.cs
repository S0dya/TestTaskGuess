using DG.Tweening;
using System;
using UnityEngine;

namespace Game.Animation
{
    public class FadeOutAnimation : BaseAnimation
    {
        [SerializeField]
        private float _finalAlpha = 0;

        [SerializeField]
        private float _fadeOutDuration = 1;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        public override void StopAnimation()
        {
            base.StopAnimation();

            _canvasGroup.alpha = _finalAlpha;
            _canvasGroup.blocksRaycasts = false;
        }

        public override void PlayAnimation(Action onAnimationEnded = null)
        {
            base.StopAnimation();

            _canvasGroup.blocksRaycasts = false;

            _tweener = _canvasGroup.DOFade(_finalAlpha, _fadeOutDuration).OnComplete(() =>
            {
                onAnimationEnded?.Invoke();
            });
        }
    }
}
