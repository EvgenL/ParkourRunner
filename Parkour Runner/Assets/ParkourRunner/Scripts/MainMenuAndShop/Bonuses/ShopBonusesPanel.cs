using UnityEngine;
using UnityEngine.UI;
using AEngine;

public class ShopBonusesPanel : MonoBehaviour
{
    [SerializeField] private Image _bonusImg;
    [SerializeField] private Text _price;
    [SerializeField] private Button _buyBtn;
    [SerializeField] private GameObject _unitsPlace;
    [SerializeField] private Sprite _activeUnit;
    [SerializeField] private Sprite _deactiveUnit;
    [SerializeField] private GameObject _coinImg;
    
    private int _purchasedBonusesCount;
    private int _bonusRestoreValue;
    
    public Image MyImage
    {
        get { return _bonusImg; }
        set { _bonusImg = value; }
    }

    public BonusName BonusKind { get; set; }

    public int[] Prices { get; set; }

    public Text MyPrice
    {
        get { return _price; }
        set { _price = value; }
    }

    private void Start()
    {
        _purchasedBonusesCount = GetPurchasedBonusesCount();
        _bonusRestoreValue = GetPurchasedBonusesCount();
        SetUnitsImgs();
        PossibilityOfPurchase();
    }

    public void Buy()
    {
        int bonusLevel = PlayerPrefs.GetInt(this.BonusKind.ToString());
        
        if (bonusLevel < this.Prices.Length)
        {
            int price = this.Prices[bonusLevel];

            if (Wallet.Instance.SpendCoins(price))
            {
                Shoping.GetBonus(this.BonusKind.ToString());
                RefreshPrice();
                SetUnitsToActive();
                AudioManager.Instance.PlaySound(Sounds.UpgradeBonus);
            }
        }

        PossibilityOfPurchase();
    }

    public void RefreshPrice()
    {
        int bonusLevel = Mathf.Clamp(PlayerPrefs.GetInt(this.BonusKind.ToString()), 0, this.Prices.Length - 1);

        _price.text = this.Prices[bonusLevel].ToString();
    }
    
    private int GetPurchasedBonusesCount()
    {
        return PlayerPrefs.GetInt(this.BonusKind.ToString());
    }

    private void SetUnitsImgs()
    {
        for (int i = 0; i < _unitsPlace.transform.childCount; i++)
        {
            if (i < _purchasedBonusesCount)
            {
                _unitsPlace.transform.GetChild(i).GetComponent<Image>().sprite = _activeUnit;
            }
            else
            {
                _unitsPlace.transform.GetChild(i).GetComponent<Image>().sprite = _deactiveUnit;
            }
        }
    }
    
    private void SetUnitsToActive()
    {
        for (int i = 0; i < _unitsPlace.transform.childCount; i++)
        {
            if (_unitsPlace.transform.GetChild(i).GetComponent<Image>().sprite == _deactiveUnit)
            {
                _unitsPlace.transform.GetChild(i).GetComponent<Image>().sprite = _activeUnit;
                return;
            }
        }
    }

    private void PossibilityOfPurchase()
    {
        int bonusLevel = PlayerPrefs.GetInt(this.BonusKind.ToString());

        if (bonusLevel == 10)
        {
            _coinImg.GetComponent<Image>().enabled = false;
            _buyBtn.gameObject.GetComponent<Image>().enabled = false;
            _price.enabled = false;
        }
        if (bonusLevel != 10)
        {
            _coinImg.GetComponent<Image>().enabled = true;
            _buyBtn.gameObject.GetComponent<Image>().enabled = true;
            _price.enabled = true;
        }
    }
}
