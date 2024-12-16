using TMPro;
using UnityEngine;

public class UITaskDescriptionController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _descriptionText;

    public void SetDescription(string text)
    {
        _descriptionText.text = "Find " + text;
    }
}
