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
    
    private AudioManager _audio;

    private void Awake()
    {
        _audio = AudioManager.Instance;
    }

    private void Start()
    {
        _donatTab.button.onClick.AddListener(() => OnSelectShopClisk(ShopsType.donatShop, true));
        _bonusesTab.button.onClick.AddListener(() => OnSelectShopClisk(ShopsType.bonusShop, true));
        
        OnActivateDefaultTab(false);
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
    }

    private void SetImageState(Image image, bool state)
    {
        Color color = image.color;
        color.a = state ? 1f : 0f;
        image.color = color;
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
            case ShopsType.donatShop:
                ActivateTargetShop(_allShops[(int)ShopsType.donatShop]);
                ActivateTargetTab(_donatTab);
                break;

            case ShopsType.bonusShop:
                ActivateTargetShop(_allShops[(int)ShopsType.bonusShop]);
                ActivateTargetTab(_bonusesTab);
                break;
                
            default:
                break;
        }

        if (playSound)
        {
            _audio.PlaySound(Sounds.Tap);
        }
    }
    #endregion
}