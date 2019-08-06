using UnityEngine;

public class LocalizationComponent : MonoBehaviour
{
    [SerializeField] private string _defaultText;
    [SerializeField] private string _key;

    public string Text
    {
        get
        {
            if (!LocalizationManager.Instance.LockLocalization)
            {
                string txt = LocalizationManager.Instance.GetText(_key);
                return string.IsNullOrEmpty(txt) ? _defaultText : txt;
            }

            Debug.Log("Localization key " + _key + "was not found or used debug mode");

            return _defaultText;
        }
    }
}