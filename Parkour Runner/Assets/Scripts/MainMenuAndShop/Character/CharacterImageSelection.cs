using UnityEngine;
using UnityEngine.UI;

public class CharacterImageSelection : MonoBehaviour
{
    [SerializeField] private CharactersData _configuration;
    [SerializeField] private CharacterKinds _kind;
    [SerializeField] private Image _selection;

    private CharactersData.Data _data;

    private void Awake()
    {
        _data = _configuration.GetCharacterData(_kind);

        _selection.enabled = _data.Bought && _configuration.CurrentCharacter == _kind;
    }

    public bool Enable { get { return _data.Bought && _configuration.CurrentCharacter == _kind; } set { _selection.enabled = value; } }

    public bool CanSelect { get { return _data.Bought; } }

    public void OnClick()
    {

    }
}
