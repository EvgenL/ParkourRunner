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
    private bool _isInitialized = false;
    
    private void OnEnable()
    {
        _purchaseManager = InAppManager.Instance;

        if (_coinsText != null)
            _coinsText.text = _donatData.DonatValue;

        if (!_isInitialized)
            StartCoroutine(LoadingDataProcess());

        InAppManager.OnBuySuccess += OnBuySuccess;
    }

    private void OnDisable()
    {
        InAppManager.OnBuySuccess -= OnBuySuccess;
    }

    private IEnumerator LoadingDataProcess()
    {
        while (!_purchaseManager.IsInitialized())
            yield return null;
        
        RefreshProductData();

        _isInitialized = true;
    }

    private void RefreshProductData()
    {
        if (_purchasePrice != null)
            _purchasePrice.text = _purchaseManager.GetLocalizedPrice(_donatData.ProductGameId);
        if (_purchaseCurrency != null)
            _purchaseCurrency.text = _purchaseManager.GetLocalizedCurrency(_donatData.ProductGameId);
    }

    #region Events
    public void OnBuyButtonClick()
    {
        AudioManager.Instance.PlaySound(Sounds.Tap);
        _purchaseManager.BuyProductID(_donatData.ProductGameId);
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