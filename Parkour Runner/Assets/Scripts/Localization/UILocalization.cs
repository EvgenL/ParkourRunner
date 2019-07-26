using UnityEngine;
using UnityEngine.UI;

public class UILocalization : MonoBehaviour
{
    [SerializeField] private string _key;
    [SerializeField] private Text _text;

    private void Start()
    {
        if (!LocalizationManager.Instance.LockLocalization)
        {
            string txt = LocalizationManager.Instance.GetText(_key);

            if (!string.IsNullOrEmpty(txt))
                _text.text = txt;
        }
    }
}