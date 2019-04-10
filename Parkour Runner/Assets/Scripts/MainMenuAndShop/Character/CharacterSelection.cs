using System;
using UnityEngine;
using UnityEngine.UI;
using AEngine;

public class CharacterSelection : MonoBehaviour
{
    private static event Action<CharacterKinds> OnSelectCharacter;

    [SerializeField] private CharactersData _configuration;
    [SerializeField] private CharacterKinds _kind;
    [SerializeField] private CharacterImageSelection _avatarImage;

    [Header("Buy block")]
    [SerializeField] private GameObject _priceBlock;
    [SerializeField] private Text _priceText;
    [SerializeField] private GameObject _selectCaption;

    private CharactersData.Data _data;
    private Wallet _wallet;

    private bool EnableCharacter
    {
        get { return _data != null && _data.Bought && _configuration.CurrentCharacter == _kind; }
        set
        {
            if (value)
            {
                PlayerPrefs.SetString(CharactersData.CHARACTER_KEY, _kind.ToString());
                PlayerPrefs.Save();
            }
            
            _avatarImage.Enable = value;
        }
    }

    private void Awake()
    {
        _data = _configuration.GetCharacterData(_kind);
        _priceText.text = _data.price.ToString();

        _priceBlock.SetActive(!_data.Bought);
        _selectCaption.SetActive(_data.Bought);
        
        _wallet = Wallet.Instance;

        _avatarImage.OnAvatarImageClick += OnSelectButtonClick;
    }

    private void OnEnable()
    {
        OnSelectCharacter -= OnSelectCharacterHandle;
        OnSelectCharacter += OnSelectCharacterHandle;
    }

    private void OnDisable()
    {
        OnSelectCharacter -= OnSelectCharacterHandle;
    }

    private void Start()
    {
        if (this.EnableCharacter)
            OnSelectCharacter.SafeInvoke(_kind);
    }
        
    public void OnSelectButtonClick()
    {
        if (!_data.Bought && _wallet.SpendCoins(_data.price))
        {
            AudioManager.Instance.PlaySound(Sounds.ShopSlot);
            _data.Bought = true;
            OnSelectCharacter.SafeInvoke(_kind);

            _priceBlock.SetActive(!_data.Bought);
            _selectCaption.SetActive(_data.Bought);
        }
        else if (_data.Bought && !this.EnableCharacter)
        {
            AudioManager.Instance.PlaySound(Sounds.Bonus);
            OnSelectCharacter.SafeInvoke(_kind);
        }
    }

    private void OnSelectCharacterHandle(CharacterKinds kind)
    {
        this.EnableCharacter = _kind == kind;
    }
}
