using Game.Animation;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class UILevelFinish : MonoBehaviour
    {
        [SerializeField]
        private BaseAnimation _fadeInAnimation;

        [SerializeField]
        private BaseAnimation _fadeOutAnimation;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        [Inject]
        private UILoadingScreen _uiLoadingScreen;

        public void OpenTab()
        {
            _fadeOutAnimation.StopAnimation();
            _fadeInAnimation.PlayAnimation();
        }

        public void ButtonRestartPressed()
        {
            if (_canvasGroup.alpha < 1) return;

            _fadeInAnimation.StopAnimation();

            _uiLoadingScreen.OpenTab(() => _fadeOutAnimation.PlayAnimation());
        }
    }
}
