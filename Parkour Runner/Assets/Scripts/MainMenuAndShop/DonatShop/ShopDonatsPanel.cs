using UnityEngine;
using UnityEngine.UI;
using AEngine;

public class ShopDonatsPanel : MonoBehaviour
{
    [SerializeField] private InAppManager _purchaseManager;
    [SerializeField] private DonatShopData _donatData;
    [SerializeField] private Text _coinsText;
    
    private void Start()
    {
        if (_coinsText != null)
            _coinsText.text = _donatData.DonatValue;
    }

    private void OnEnable()
    {
        InAppManager.OnBuySuccess -= OnBuySuccess;
        InAppManager.OnBuySuccess += OnBuySuccess;
    }

    private void OnDisable()
    {
        InAppManager.OnBuySuccess -= OnBuySuccess;
    }
    
    #region Events
    private void OnBuySuccess(InAppManager.ProductConfiguration product)
    {
        if (_donatData.DonatKind == product.data.DonatKind)
        {
            Shoping.GetDonat(_donatData);
            AudioManager.Instance.PlaySound(Sounds.ShopSlot);
        }
    }
    #endregion
}

