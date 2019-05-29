using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using AEngine;

public class ShopDonatsPanel : MonoBehaviour
{
    [SerializeField] private DonatShopData _donatData;
    [SerializeField] private Button _buyButton;

    [Header("UI view")]
    [SerializeField] private Text _coinsText;
    [SerializeField] private Text _purchasePrice;
    [SerializeField] private Text _purchaseCurrency;

    private InAppManager _purchaseManager;

    private void Start()
    {
        _purchaseManager = AppManager.Instance.PurchaseManager;

        if (_coinsText != null)
            _coinsText.text = _donatData.DonatValue;

        _buyButton.onClick.AddListener(() => _purchaseManager.BuyProductID(_donatData.ProductGameId));

        InAppManager.OnInitializationSuccess += OnInitializePurchaseSuccess;
        InAppManager.OnBuySuccess += OnBuySuccess;
    }
    
    #region Events
    private void OnInitializePurchaseSuccess()
    {
        if (_purchasePrice != null)
            _purchasePrice.text = _purchaseManager.GetLocalizedPrice(_donatData.ProductGameId);
        if (_purchaseCurrency != null)
            _purchaseCurrency.text = _purchaseManager.GetLocalizedCurrency(_donatData.ProductGameId);
    }

    private void OnBuySuccess(DonatShopData.DonatKinds productKind)
    {
        if (_donatData.DonatKind == productKind)
        {
            Shoping.GetDonat(_donatData);
            AudioManager.Instance.PlaySound(Sounds.ShopSlot);
        }
    }
    #endregion
}