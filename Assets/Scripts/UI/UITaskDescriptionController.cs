using Game.Animation;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class UITaskDescriptionController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _descriptionText;

        [SerializeField]
        private BaseAnimation _fadeInAnimation;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        public void SetDescription(string text, bool animate)
        {
            _descriptionText.text = "Find " + text;

            if (animate)
            {
                _fadeInAnimation.PlayAnimation();
            }
        }

        public void ClearDescription()
        {
            _descriptionText.text = "";
            _canvasGroup.alpha = 0;
        }
    }
}
