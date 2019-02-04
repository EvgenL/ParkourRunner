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
    private string _name;
    private int _purchasedBonusesCount;
    private int _bonusRestoreValue;
    
    public Image MyImage
    {
        get
        {
            return _bonusImg;
        }

        set
        {
            _bonusImg = value;
        }
    }

    public Text MyPrice
    {
        get
        {
            return _price;
        }
        set
        {
            _price = value;
        }
    }

    public string MyName
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    private void Start()
    {
        _purchasedBonusesCount = GetPurchasedBonusesCount();
        _bonusRestoreValue = GetPurchasedBonusesCount();
        SetUnitsImgs();
        PossibilityOfPurchase();
    }

    public void BuyThisThing()
    {
        if (PlayerPrefs.GetInt(gameObject.name) < 10)
        {
            Shoping.GetBonus(gameObject.name);
            SetUnitsToActive();
            AudioManager.Instance.PlaySound(Sounds.UpgradeBonus);
        }
        PossibilityOfPurchase();
    }

    private int GetPurchasedBonusesCount()
    {
        return PlayerPrefs.GetInt(gameObject.name);
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
        if (PlayerPrefs.GetInt(gameObject.name) == 10)
        {
            _coinImg.GetComponent<Image>().enabled = false;
            _buyBtn.gameObject.GetComponent<Image>().enabled = false;
            _price.enabled = false;
        }
        if (PlayerPrefs.GetInt(gameObject.name) != 10)
        {
            _coinImg.GetComponent<Image>().enabled = true;
            _buyBtn.gameObject.GetComponent<Image>().enabled = true;
            _price.enabled = true;
        }
    }
}
