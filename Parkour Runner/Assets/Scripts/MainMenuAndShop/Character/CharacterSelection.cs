using UnityEngine;
using UnityEngine.UI;
using AEngine;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private CharactersData _configuration;
    [SerializeField] private CharacterKinds _kind;
    [SerializeField] private CharacterImageSelection _avatarImage;

    [Header("Buy block")]
    [SerializeField] private GameObject _priceBlock;
    [SerializeField] private Text _priceText;
    [SerializeField] private GameObject _selectCaption;

    [Header("Pivot block")]
    [SerializeField] private RectTransform _avatar;
    [SerializeField] private RectTransform _avatarGroupPivot;
    [SerializeField] private RectTransform _avatarYPivot;

    private CharactersData.Data _data;

    private void Awake()
    {
        _data = _configuration.GetCharacterData(_kind);
        _priceText.text = _data.price.ToString();
    }

    private void Update()
    {
        Vector3 position = _avatarGroupPivot.position;
        position.x = _avatarYPivot.position.x;
        _avatar.position = position;
    }

    public void OnSelectButtonClick()
    {
        

        PlayerPrefs.SetString(CharactersData.CHARACTER_KEY, _kind.ToString());
        PlayerPrefs.Save();

        _avatarImage.Enable = true;

        AudioManager.Instance.PlaySound(Sounds.Bonus);
    }
}
