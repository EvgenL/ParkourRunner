using System;
using UnityEngine;

public class CharactersView : MonoBehaviour
{
    [Serializable]
    private struct CharacterBlock
    {
        public GameObject character;
        public CharacterKinds kind;
    }

    [SerializeField] private CharactersData _data;
    [SerializeField] private CharacterBlock[] _characters;

    private void Start()
    {
        OnSelectCharacter(_data.CurrentCharacter);
    }

    private void OnEnable()
    {
        CharacterSelection.OnSelectCharacter += OnSelectCharacter;
    }

    private void OnDisable()
    {
        CharacterSelection.OnSelectCharacter -= OnSelectCharacter;
    }

    #region Events
    private void OnSelectCharacter(CharacterKinds kind)
    {
        foreach (CharacterBlock item in _characters)
        {
            item.character.SetActive(item.kind == kind);
        }
    }
    #endregion
}