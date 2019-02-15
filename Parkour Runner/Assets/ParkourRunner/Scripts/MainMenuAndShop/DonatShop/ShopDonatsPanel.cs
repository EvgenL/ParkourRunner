using UnityEngine;
using UnityEngine.UI;
using AEngine;

public class ShopDonatsPanel : MonoBehaviour
{
    [SerializeField] private DonatShopData _donatData;
    [SerializeField] private Button _buyBtn;
            
    private void Start()
    {
        if (_donatData.DonatKind == DonatShopData.DonatKinds.NoAds && PlayerPrefs.GetInt(_donatData.DonatKind.ToString()) == 1)
        {
            _buyBtn.interactable = false;
        }
    }

    public void BuyThisThing()
    {
        if (_donatData.DonatKind == DonatShopData.DonatKinds.NoAds)
        {
            _buyBtn.interactable = false;
        }

        Shoping.GetDonat(_donatData);

        AudioManager.Instance.PlaySound(Sounds.ShopSlot);
    }
}

