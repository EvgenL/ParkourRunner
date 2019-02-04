using UnityEngine;
using UnityEngine.UI;
using AEngine;

public class ShopDonatsPanel : MonoBehaviour
{
    [SerializeField] private Image _donatImg;
    [SerializeField] private Button _buyBtn;

    public Image MyImage
    {
        get
        {
            return _donatImg;
        }

        set
        {
            _donatImg = value;
        }
    }

    public string MyName { get; set; }

    private void Start()
    {
        if (gameObject.name == "NoAds" && PlayerPrefs.GetInt(gameObject.name)==1)
        {
            _buyBtn.gameObject.GetComponent<Image>().enabled = false;
        }
    }

    public void BuyThisThing()
    {
        if (gameObject.name == "NoAds")
        {
            _buyBtn.gameObject.GetComponent<Image>().enabled = false;
        }

        Shoping.GetDonat(gameObject.name);

        AudioManager.Instance.PlaySound(Sounds.ShopSlot);
    }
}

