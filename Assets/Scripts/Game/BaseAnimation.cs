using UnityEngine;
using DG.Tweening;

public abstract class BaseAnimation : MonoBehaviour
{
    protected Tweener _tweener;

    public virtual void StopAnimation()
    {
        _tweener?.Kill();
    }

    public abstract void PlayAnimation();
}