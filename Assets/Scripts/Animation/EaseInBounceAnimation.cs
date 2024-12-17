using UnityEngine;
using DG.Tweening;
using System;

namespace Game.Animation
{
    public class EaseInBounceAnimation : BaseAnimation
    {
        [SerializeField]
        private float _shakeStrength = 30f;

        [SerializeField]
        private int _vibrato = 4;

        [SerializeField]
        private float _duration = 0.5f;

        public override void PlayAnimation(Action onAnimationEnded = null)
        {
            StopAnimation();

            _tweener = transform.DOShakePosition(
                _duration, new Vector3(_shakeStrength, 0, 0), _vibrato, 90, false, true)
                .OnComplete(() =>
                {
                    onAnimationEnded?.Invoke();
                });
        }
    }
}
