using Game.Animation;
using System;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class UILoadingScreen : MonoBehaviour
    {
        [SerializeField]
        private BaseAnimation _fadeInAnimation;

        [SerializeField]
        private BaseAnimation _fadeOutAnimation;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        [Inject] 
        private LevelManager _levelManager;

        public void OpenTab(Action onTabOpened)
        {
            _fadeOutAnimation.StopAnimation();
            _fadeInAnimation.PlayAnimation(() => 
            { 
                _levelManager.ClearLevel();

                CloseTab();

                onTabOpened?.Invoke();
            });
        }

        public void CloseTab()
        {
            _fadeInAnimation.StopAnimation();
            _fadeOutAnimation.PlayAnimation(_levelManager.StartNewLevel);
        }
    }
}
