using System;
using UnityEngine;
using UnityEngine.UI;
using AEngine;

public class CharacterSelection : MonoBehaviour
{
    public static event Action<CharacterKinds> OnSelectCharacter;
    private static CharacterKinds _currentSelection;
    
    [SerializeField] private CharactersData _configuration;
    [SerializeField] private CharacterKinds _kind;
    [SerializeField] private Image _selection;
    [SerializeField] private Sprite _selectedSpite;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private Sprite _disableSprite;
    
    [Header("Buy block")]
    [SerializeField] private Text _priceText;
    [SerializeField] private GameObject _priceBlock;
    [SerializeField] private GameObject _lockBlock;
    [SerializeField] private GameObject _selectCaption;
    [SerializeField] private GameObject _buyCaption;
    [SerializeField] private Button _button;

    private CharactersData.Data _data;
    private Wallet _wallet;

    private void Awake()
    {
        _data = _configuration.GetCharacterData(_kind);
        _priceText.text = _data.price.ToString();

        _priceBlock.SetActive(!_data.Bought);
        _selectCaption.SetActive(_data.Bought);

        _wallet = Wallet.Instance;
    }

    private void OnEnable()
    {
        OnSelectCharacter -= OnSelectCharacterHandle;
        OnSelectCharacter += OnSelectCharacterHandle;

        if (_configuration.CurrentCharacter == _kind)
            OnSelectCharacter.SafeInvoke(_kind);
    }

    private void OnDisable()
    {
        OnSelectCharacter -= OnSelectCharacterHandle;
    }

    private void Start()
    {
        if (_configuration.CurrentCharacter == _kind)
        {
            _currentSelection = _kind;
            OnSelectCharacter.SafeInvoke(_kind);
        }
    }

    private void RefreshSelection(CharacterKinds kind)
    {
        if (_currentSelection == _kind)
        {
            _selection.sprite = _selectedSpite;
        }
        else
        {
            _selection.sprite = _configuration.CurrentCharacter == _kind ? _activeSprite : _disableSprite;
        }

        _priceBlock.SetActive(!_data.Bought);
        _lockBlock.SetActive(!_data.Bought);
        _buyCaption.SetActive(!_data.Bought);
        _button.interactable = _data.Bought || _wallet.AllCoins >= _data.price;

        _selectCaption.SetActive(_data.Bought);
    }
        
    public void OnSelectButtonClick()
    {
        if (_currentSelection != _kind)
            AudioManager.Instance.PlaySound(Sounds.Bonus);

        _currentSelection = _kind;

        if (_data.Bought)
        {
            PlayerPrefs.SetString(CharactersData.CHARACTER_KEY, _kind.ToString());
            PlayerPrefs.Save();
        }

        OnSelectCharacter.SafeInvoke(_kind);
    }

    public void OnBuyButtonClick()
    {
        if (!_data.Bought && _wallet.SpendCoins(_data.price))
        {
            AudioManager.Instance.PlaySound(Sounds.ShopSlot);

            PlayerPrefs.SetString(CharactersData.CHARACTER_KEY, _kind.ToString());
            PlayerPrefs.Save();

            _currentSelection = _kind;
            _data.Bought = true;
                        
            OnSelectCharacter.SafeInvoke(_kind);
        }
        else if (_data.Bought)
            OnSelectButtonClick();
    }

    private void OnSelectCharacterHandle(CharacterKinds kind)
    {
        RefreshSelection(kind);
    }
}