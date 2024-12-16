using UnityEngine;
using DG.Tweening;

public class BounceAnimation : BaseAnimation
{
    [SerializeField]
    private Vector3 _bounceStrength = new Vector3(0, 1, 0);
    [SerializeField]
    private float _duration = 1f;

    public override void PlayAnimation()
    {
        StopAnimation();
        _tweener = transform.DOShakePosition(_duration, _bounceStrength);
    }
}
