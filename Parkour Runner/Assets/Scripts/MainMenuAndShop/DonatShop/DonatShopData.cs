using UnityEngine;
using UnityEngine.Purchasing;

[CreateAssetMenu(fileName = "New DonatShopData", menuName = "DonatShopData", order = 52)]
public class DonatShopData : ScriptableObject
{
    public enum DonatKinds
    {
        NoAds,
        ByCoins1,
        ByCoins2,
        ByCoins3,
        ByCoins4
    }

    [Header("Base settings")]
    [SerializeField] private DonatKinds _kind;
    [SerializeField] private string _donatValue;

    [Header("Unity In-App settings")]
    [SerializeField] private string _productGameId;
    [SerializeField] private string _productAppStoreId;
    [SerializeField] private string _productGooglePlayId;
    [SerializeField] private ProductType _productType;

    public DonatKinds DonatKind { get { return _kind; } }

    public string DonatValue { get { return _donatValue; } }

    public string ProductGameId { get { return _productGameId; } }

    public string ProductAppStoreId { get { return _productAppStoreId; } }

    public string ProductGooglePlayId { get { return _productGooglePlayId; } }

    public ProductType PurchaseType { get { return _productType; } }
}