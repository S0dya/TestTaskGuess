using DG.Tweening;
using System;
using UnityEngine;

namespace Game.UI.Elements
{
    public class UITabHandler : MonoBehaviour
    {
        [Header("Settings")]
        [Min(0)][SerializeField] private float fadeInDuration;
        [Min(0)][SerializeField] private float fadeOutDuration = 0.2f;

        [SerializeField] private bool initOn;

        [Header("Components")]
        [SerializeField] private CanvasGroup[] canvasGroups;

        private Sequence _curSequence;

        private void Awake()
        {
            if (canvasGroups == null || canvasGroups.Length == 0)
                canvasGroups = new CanvasGroup[] { GetComponent<CanvasGroup>() };

            foreach (var cg in canvasGroups)
            {
                cg.alpha = initOn ? 1 : 0;
                ToggleObjects(initOn);
                cg.blocksRaycasts = initOn;
            }
        }

        public void OpenTab(Action onTabOpened = null)
        {
            KillSequence();

            ToggleObjects(true);

            _curSequence = DOTween.Sequence();
            foreach (var cg in canvasGroups)
            {
                _curSequence.Join(cg.DOFade(1, fadeInDuration));
            }
            _curSequence.OnComplete(() =>
            {
                if (this == null || canvasGroups == null) return;

                foreach (var cg in canvasGroups) cg.blocksRaycasts = true;

                onTabOpened?.Invoke();
            });
        }

        public void CloseTab(Action onTabClosed = null)
        {
            KillSequence();

            foreach (var cg in canvasGroups) cg.blocksRaycasts = false;

            _curSequence = DOTween.Sequence();
            foreach (var cg in canvasGroups)
            {
                _curSequence.Join(cg.DOFade(0, fadeOutDuration));
            }
            _curSequence.OnComplete(() =>
            {
                if (this == null || canvasGroups == null) return;

                onTabClosed?.Invoke();
                ToggleObjects(false);
            });
        }

        private void KillSequence()
        {
            if (_curSequence != null && _curSequence.IsActive())
            {
                _curSequence.Kill(); _curSequence = null;
            }
        }

        public bool TabIsOpened() => canvasGroups[0].alpha >= 1;

        private void ToggleObjects(bool toggle)
        {
            foreach (var cg in canvasGroups)
            {
                cg.gameObject.SetActive(toggle);
            }
        }
    }
}