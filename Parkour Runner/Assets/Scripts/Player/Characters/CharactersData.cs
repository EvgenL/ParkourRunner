using System;
using UnityEngine;

public enum CharacterKinds
{
    Base,
    Character2,
    Character3,
    Character4,
    CaptainAmerica
}

[CreateAssetMenu(fileName = "Character Configuration", menuName = "Character Data", order = 50)]
public class CharactersData : ScriptableObject
{
    public const string CHARACTER_KEY = "Character";

    [Serializable]
    public class Data
    {
        public CharacterKinds kind;
        public GameObject targetPrefab;
        public int price;

        public string Key { get { return CHARACTER_KEY + " : " + kind.ToString(); } }

        public bool Bought
        {
            get
            {
                if (PlayerPrefs.HasKey(this.Key))
                {
                    return PlayerPrefs.GetInt(this.Key) != 0 || price <= 0;
                }
                else
                {
                    bool bought = price <= 0;
                    PlayerPrefs.SetInt(this.Key, bought ? 1 : 0);
                    PlayerPrefs.Save();
                    return bought;
                }
            }

            set
            {
                PlayerPrefs.SetInt(this.Key, value || price <= 0 ? 1 : 0);
                PlayerPrefs.Save();
            }
        }
    }

    [SerializeField] private Data[] _characters;

    public CharacterKinds CurrentCharacter
    {
        get
        {
            if (!PlayerPrefs.HasKey(CHARACTER_KEY))
            {
                PlayerPrefs.SetString(CHARACTER_KEY, CharacterKinds.Base.ToString());
                PlayerPrefs.Save();
            }

            return (CharacterKinds)Enum.Parse(typeof(CharacterKinds), PlayerPrefs.GetString(CHARACTER_KEY));
        }
    }

    public Data GetCharacterData(CharacterKinds kind)
    {
        Data data = null;

        if (_characters != null && _characters.Length > 0)
        {
            foreach (Data item in _characters)
            {
                if (item.kind == kind)
                {
                    data = item;
                    break;
                }
            }

            if (data == null)
            {
                data = _characters[0];
                Debug.LogError("Couldn't find or not configured character = " + kind.ToString());
            }
        }

        return data;
    }    
}
