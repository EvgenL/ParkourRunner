using UnityEngine;
using UnityEngine.UI;
using AEngine;
using System;

public  class BaseShop : MonoBehaviour
{
    public enum ShopsType
    {
        charShop,
        coinsShop,
        bonusShop
    }

    [Serializable]
    private class Tab
    {
        public Button button;
        public Image image;
        public Sprite enable;
        public Sprite disable;
    }

    [SerializeField] private GameObject[] _allShops;
    [SerializeField] private GameObject _notEnoughCoinsWindow;
    [SerializeField] private Text _notEnougtCoins;
    [SerializeField] private Tab _charTab;
    [SerializeField] private Tab _coinstTab;
    [SerializeField] private Tab _bonusesTab;
    
    
    private AudioManager _audio;

    private void Awake()
    {
        _audio = AudioManager.Instance;
    }

    private void Start()
    {
        _charTab.button.onClick.AddListener(() => OnSelectShopClisk(ShopsType.charShop, true));
        _coinstTab.button.onClick.AddListener(() => OnSelectShopClisk(ShopsType.coinsShop, true));
        _bonusesTab.button.onClick.AddListener(() => OnSelectShopClisk(ShopsType.bonusShop, true));
        
        OnActivateDefaultTab(false);
    }

    private void OnEnable()
    {
        CharacterSelection.OnNotEnouthCoins += OnShowNotEnoughWindow;
    }

    private void OnDisable()
    {
        CharacterSelection.OnNotEnouthCoins -= OnShowNotEnoughWindow;
    }

    private void ActivateTargetShop(GameObject shop)
    {
        foreach (var item in _allShops)
        {
            item.SetActive(item == shop);
        }
    }

    private void ActivateTargetTab(Tab target)
    {
        _charTab.image.sprite = _charTab == target ? _charTab.enable : _charTab.disable;
        _coinstTab.image.sprite = _coinstTab == target ? _coinstTab.enable : _coinstTab.disable;
        _bonusesTab.image.sprite = _bonusesTab == target ? _bonusesTab.enable : _bonusesTab.disable;
    }
    
    #region Events
    public void OnActivateDefaultTab(bool playSound)
    {
        OnSelectShopClisk(ShopsType.bonusShop, playSound);
    }

    private void OnSelectShopClisk(ShopsType shop, bool playSound)
    {
        switch (shop)
        {
            case ShopsType.coinsShop:
                ActivateTargetShop(_allShops[(int)ShopsType.coinsShop]);
                ActivateTargetTab(_coinstTab);
                break;

            case ShopsType.bonusShop:
                ActivateTargetShop(_allShops[(int)ShopsType.bonusShop]);
                ActivateTargetTab(_bonusesTab);
                break;

            case ShopsType.charShop:
                ActivateTargetShop(_allShops[(int)ShopsType.charShop]);
                ActivateTargetTab(_charTab);
                break;
                
            default:
                break;
        }

        if (playSound)
        {
            _audio.PlaySound(Sounds.Tap);
        }
    }

    public void OnShowNotEnoughWindow(int coins)
    {
        _notEnougtCoins.text = coins.ToString();
        _notEnoughCoinsWindow.SetActive(true);
    }

    public void OnBuyNotEnoughWindowClick()
    {
        _audio.PlaySound(Sounds.Tap);

        ActivateTargetShop(_allShops[(int)ShopsType.coinsShop]);
        ActivateTargetTab(_coinstTab);

        _notEnoughCoinsWindow.SetActive(false);
    }

    public void OnCloseNotEnoughCoinsWindow()
    {
        _audio.PlaySound(Sounds.Tap);
        _notEnoughCoinsWindow.SetActive(false);
    }
    #endregion
}