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
    }

    [SerializeField] private Data[] _characters;

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
