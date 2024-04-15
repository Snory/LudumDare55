using TMPro;
using UnityEngine;

public class TutorialButtonNextText : MonoBehaviour
{
    [SerializeField]    
    private TextMeshProUGUI _textField;

    private void OnEnable()
    {
        //OnNextText();
    }

    public void OnNextText()
    {
        var text = TutorialManager.Instance.NextText();

        if (text == null)
            return;

        _textField.text = text;
    }
}
