using System;
using UnityEngine;

public class CharacterKindController : MonoBehaviour
{
    [SerializeField] private GameObject _camera;

    [Tooltip("It must be true (load character by player choose). Use false as debug mode with Kind property below.")]
    [SerializeField] private bool _selectBySettings = true;

    [Tooltip("Use as debug mode with disabled property SelectBySettings.")]
    [SerializeField] private CharacterKinds _kind;

    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private CharactersData _data;
    
    private void Awake()
    {
        _camera.SetActive(false);
                
        CharactersData.Data targetData = _data.GetCharacterData(_selectBySettings ? LoadChoosenKind() : _kind);
                
        if (targetData != null)
        {
            GameObject character = GameObject.Instantiate(targetData.targetPrefab, _startPosition, Quaternion.identity);
        }

        _camera.SetActive(true);
    }

    private CharacterKinds LoadChoosenKind()
    {
        if (!PlayerPrefs.HasKey(CharactersData.CHARACTER_KEY))
        {
            PlayerPrefs.SetString(CharactersData.CHARACTER_KEY, CharacterKinds.Base.ToString());
            PlayerPrefs.Save();
        }

        return (CharacterKinds)Enum.Parse(typeof(CharacterKinds), PlayerPrefs.GetString(CharactersData.CHARACTER_KEY));
    }
}