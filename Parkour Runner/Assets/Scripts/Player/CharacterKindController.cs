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

public class CharacterKindController : MonoBehaviour
{
    [Serializable]
    private class CharacterKindData
    {
        public CharacterKinds kind;
        public GameObject targetPrefab;
    }

    [SerializeField] private GameObject _camera;
    [SerializeField] private bool _selectBySettings = true;
    [Tooltip("Use as debug mode. It have to load by player choose in menu.")]
    [SerializeField] private CharacterKinds _kind;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private CharacterKindData[] _characters;

    private void Awake()
    {
        _camera.SetActive(false);

        CharacterKindData target = _characters.Length > 0 ? _characters[0] : null;
        CharacterKinds kind = _selectBySettings ? (CharacterKinds)Enum.Parse(typeof(CharacterKinds), PlayerPrefs.GetString("Character")) : _kind;

        foreach (var item in _characters)
            if (item.kind == kind)
            {
                target = item;
                break;
            }

        if (target != null)
        {
            GameObject character = GameObject.Instantiate(target.targetPrefab, _startPosition, Quaternion.identity);
        }

        _camera.SetActive(true);
    }
}