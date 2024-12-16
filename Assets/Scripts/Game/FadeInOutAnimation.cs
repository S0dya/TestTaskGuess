using UnityEngine;
using DG.Tweening;

public class FadeInOutAnimation : BaseAnimation
{
    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private float _fadeDuration = 1f;

    private void Awake()
    {
        if (_canvasGroup == null)
            _canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void PlayAnimation()
    {
        StopAnimation();
        _canvasGroup.alpha = 0;
        _tweener = _canvasGroup.DOFade(1, _fadeDuration).SetLoops(2, LoopType.Yoyo);
    }
}
