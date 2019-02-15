using UnityEngine;
using UnityEngine.UI;
using AEngine;
using System;

public  class BaseShop : MonoBehaviour
{
    public enum ShopsType
    {
        donatShop,
        bonusShop,
        tricksShop
    }

    [Serializable]
    private class Tab
    {
        public Button button;
        public Image image;
    }

    [SerializeField] private GameObject[] _allShops;
    [SerializeField] private Tab _donatTab;
    [SerializeField] private Tab _bonusesTab;
    [SerializeField] private Tab _tricksTab;
    [SerializeField] private Image _firstBroad;
    [SerializeField] private Image _secondBroad;

    private AudioManager _audio;

    private void Awake()
    {
        _audio = AudioManager.Instance;
    }

    private void Start()
    {
        _donatTab.button.onClick.AddListener(() => OnSelectShopClisk(ShopsType.donatShop));
        _bonusesTab.button.onClick.AddListener(() => OnSelectShopClisk(ShopsType.bonusShop));
        _tricksTab.button.onClick.AddListener(() => OnSelectShopClisk(ShopsType.tricksShop));

        OnSelectShopClisk(ShopsType.bonusShop);
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
        SetImageState(_donatTab.image, _donatTab == target);
        SetImageState(_bonusesTab.image, _bonusesTab == target);
        SetImageState(_tricksTab.image, _tricksTab == target);

        _firstBroad.enabled = _tricksTab == target;
        _secondBroad.enabled = _donatTab == target;
    }

    private void SetImageState(Image image, bool state)
    {
        Color color = image.color;
        color.a = state ? 1f : 0f;
        image.color = color;
    }

    #region Events
    private void OnSelectShopClisk(ShopsType shop)
    {
        switch (shop)
        {
            case ShopsType.donatShop:
                ActivateTargetShop(_allShops[(int)ShopsType.donatShop]);
                ActivateTargetTab(_donatTab);
                break;

            case ShopsType.bonusShop:
                ActivateTargetShop(_allShops[(int)ShopsType.bonusShop]);
                ActivateTargetTab(_bonusesTab);
                break;

            case ShopsType.tricksShop:
                ActivateTargetShop(_allShops[(int)ShopsType.tricksShop]);
                ActivateTargetTab(_tricksTab);
                break;

            default:
                break;
        }

        _audio.PlaySound(Sounds.Tap);
    }
    #endregion
}