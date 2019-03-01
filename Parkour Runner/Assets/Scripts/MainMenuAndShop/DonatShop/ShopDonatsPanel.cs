using UnityEngine;
using UnityEngine.UI;
using AEngine;

public class ShopDonatsPanel : MonoBehaviour
{
    [SerializeField] private DonatShopData _donatData;
    [SerializeField] private Button _buyBtn;
    
    public void BuyThisThing()
    {
        Shoping.GetDonat(_donatData);

        AudioManager.Instance.PlaySound(Sounds.ShopSlot);
    }
}

