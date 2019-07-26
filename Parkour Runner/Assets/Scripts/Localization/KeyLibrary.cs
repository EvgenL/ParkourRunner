using System;
using UnityEngine;

public class KeyLibrary : MonoBehaviour
{
    [Serializable]
    private struct LocalizedText
    {
        public SystemLanguage language;
        public string text;
    }

    [Serializable]
    private struct KeyLocalization
    {
        public string key;
        public LocalizedText[] _localizations;

        public string GetText(SystemLanguage language)
        {
            foreach (var item in _localizations)
            {
                if (item.language == language)
                    return item.text;
            }

            Debug.Log(string.Format("Couldn't find text with key [{0}] and language [{1}]", key, language));

            return string.Empty;
        }
    }

    [SerializeField] private KeyLocalization[] _localizations;

    public string GetText(string key, SystemLanguage language)
    {
        foreach (var item in _localizations)
        {
            if (item.key == key)
                return item.GetText(language);
        }

        Debug.Log(string.Format("Couldn't find item with key [{0}]]", key));

        return string.Empty;
    }

    public bool HasKey(string key)
    {
        foreach (var item in _localizations)
        {
            if (item.key == key)
                return true;
        }

        return false;
    }
}