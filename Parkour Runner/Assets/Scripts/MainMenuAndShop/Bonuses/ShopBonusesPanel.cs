using UnityEngine;
using UnityEngine.UI;
using AEngine;

public class ShopBonusesPanel : MonoBehaviour
{
    [SerializeField] private BonusesShopData _data;
    [SerializeField] private Image _bonusImg;
    [SerializeField] private Text _price;
    [SerializeField] private Text _description;
    [SerializeField] private GameObject _unitsPlace;
    [SerializeField] private Sprite _activeUnit;
    [SerializeField] private Sprite _deactiveUnit;
    [SerializeField] private GameObject _coinImg;
    [SerializeField] private GameObject _fullLabel;
    
    private void Start()
    {
        SetProgressState();
        PossibilityOfPurchase();
    }

    public void Buy()
    {
        int bonusLevel = PlayerPrefs.GetInt(_data.BonusKind.ToString());
        
        if (bonusLevel < _data.Prices.Length)
        {
            int price = _data.Prices[bonusLevel];

            if (Wallet.Instance.SpendCoins(price))
            {
                AudioManager.Instance.PlaySound(Sounds.UpgradeBonus);

                Shoping.GetBonus(_data.BonusKind.ToString());
                SetProgressState();                
            }
        }

        PossibilityOfPurchase();
    }
        
    private void SetProgressState()
    {
        int progress = PlayerPrefs.GetInt(_data.BonusKind.ToString());
        
        for (int i = 0; i < _unitsPlace.transform.childCount; i++)
        {
            if (i < progress)
                _unitsPlace.transform.GetChild(i).GetComponent<Image>().sprite = _activeUnit;
            else
                _unitsPlace.transform.GetChild(i).GetComponent<Image>().sprite = _deactiveUnit;
        }

        _bonusImg.sprite = _data.GetActualIcon(progress);

        _price.text = _data.Prices[Mathf.Clamp(progress, 0, _data.Prices.Length - 1)].ToString();

        _description.text = _data.Description + string.Format(" {0}/{1}", progress, _data.Prices.Length);
    }
    
    private void PossibilityOfPurchase()
    {
        int bonusLevel = PlayerPrefs.GetInt(_data.BonusKind.ToString());
        bool enablePurchase = bonusLevel != 10;

        _coinImg.GetComponent<Image>().enabled = enablePurchase;
        _price.enabled = enablePurchase;

        _fullLabel.SetActive(!enablePurchase);
    }
}