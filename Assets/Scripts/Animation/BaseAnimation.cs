using UnityEngine;
using DG.Tweening;
using System;

namespace Game.Animation
{
    public abstract class BaseAnimation : MonoBehaviour
    {
        protected Tweener _tweener;

        public virtual void StopAnimation()
        {
            if (_tweener != null && _tweener.IsActive())
            {
                _tweener.Kill();
                _tweener = null;
            }
        }

        public abstract void PlayAnimation(Action onAnimationEnded = null);

        protected virtual void OnDestroy()
        {
            StopAnimation();
        }
    }
}