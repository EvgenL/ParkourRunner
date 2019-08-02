using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    [SerializeField] private SystemLanguage _defaultLanguage;
    [SerializeField] private List<SystemLanguage> _possibleLanguages;
    [SerializeField] private bool _lockLocalization;
    [SerializeField] private KeyLibrary[] _localizations;
    
    private SystemLanguage _language;

    public bool LockLocalization { get { return _lockLocalization; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            _language = (_possibleLanguages.Contains(Application.systemLanguage)) ? Application.systemLanguage : _defaultLanguage;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this.gameObject);
    }

    public string GetText(string key)
    {
        foreach (var item in _localizations)
        {
            if (item.HasKey(key))
                return item.GetText(key, _language);
        }

        Debug.Log(string.Format("Couldn't find library with key [{0}]", key));

        return string.Empty;
    }
}