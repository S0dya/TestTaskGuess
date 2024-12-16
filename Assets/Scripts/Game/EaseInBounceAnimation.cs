using UnityEngine;
using DG.Tweening;

public class EaseInBounceAnimation : BaseAnimation
{
    [SerializeField]
    private Vector3 _targetPosition;
    [SerializeField]
    public float _duration = 1f;

    public override void PlayAnimation()
    {
        StopAnimation();
        _tweener = transform.DOMove(_targetPosition, _duration).SetEase(Ease.InBounce);
    }
}