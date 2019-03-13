using System;
using UnityEngine.Purchasing;
using UnityEngine;

public class InAppManager : MonoBehaviour, IStoreListener
{
    [Serializable]
    public class ProductConfiguration
    {
        public string gameId;
        public string appleStoreId;
        public string googlePlayId;
        public ProductType productType;
        public DonatShopData data;
    }

    public static event Action<DonatShopData.DonatKinds> OnBuySuccess;
    public static event Action OnInitializationSuccess;

    private static IStoreController StoreController { get; set; }
    private static IExtensionProvider StoreExtensionProvider { get; set; }

    [SerializeField] private DonatShopData[] _products;
    
    private void Awake()
    {
        if (!IsInitialized())
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (var item in _products)
            {
                builder.AddProduct(item.ProductGameId, item.PurchaseType, new IDs() { { item.ProductAppStoreId, AppleAppStore.Name }, { item.ProductPlayMarketId, GooglePlay.Name } });
            }
            
            UnityPurchasing.Initialize(this, builder);
        }
    }

    public bool IsInitialized()
    {
        return StoreController != null && StoreExtensionProvider != null;
    }
    
    /// <summary>
    /// IOS only!
    /// </summary>
    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            var apple = StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) =>
            {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);

    }

    public void BuyProductID(string productId)
    {
        try
        {
            if (IsInitialized())
            {
                Product product = StoreController.products.WithID(productId);
                
                if (product != null && product.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
                    StoreController.InitiatePurchase(product);
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }
        catch (Exception e)
        {
            Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e);
        }
    }

    public bool CheckNonConsumable(string id)
    {
        return IsInitialized() && StoreController.products.WithID(id).hasReceipt;
    }

    public string GetLocalizedPrice(string id)
    {
        return IsInitialized() ? StoreController.products.WithID(id).metadata.localizedPriceString : string.Empty;
    }

    public string GetLocalizedCurrency(string id)
    {
        return IsInitialized() ? StoreController.products.WithID(id).metadata.isoCurrencyCode : string.Empty;
    }

    #region IStoreListener
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        StoreController = controller;
        StoreExtensionProvider = extensions;

        RestorePurchases();

        OnInitializationSuccess.SafeInvoke();
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed with error: " + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        foreach (var item in _products)
        {
            if (item.ProductGameId == e.purchasedProduct.definition.id)
            {
                OnBuySuccess.SafeInvoke(item.DonatKind);
                break;
            }
        }
        
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", i.definition.storeSpecificId, p));
    }
    #endregion
}
