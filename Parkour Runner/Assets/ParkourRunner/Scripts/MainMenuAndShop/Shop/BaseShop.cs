using UnityEngine;
using UnityEngine.UI;
using AEngine;

public enum ShopsType
{
    donatShop,
    bonusShop,
    tricksShop
}

public  class BaseShop : MonoBehaviour
{
    [SerializeField] private Button _donatBtn;
    [SerializeField] private Button _bonusesBtn;
    [SerializeField] private Button _tricksBtn;
    [SerializeField] private GameObject _shopsBody;
    [SerializeField] private GameObject[] _allShops;

    private AudioManager _audio;

    private void Awake()
    {
        _audio = AudioManager.Instance;
    }

    private void Start()
    {
        _shopsBody.GetComponent<Image>().color = _bonusesBtn.gameObject.GetComponent<Image>().color;
        ActiveThisShop(_allShops[(int)ShopsType.bonusShop]);
        
        _donatBtn.onClick.AddListener(() =>  ActiveCurrentShop(ShopsType.donatShop));
       _bonusesBtn.onClick.AddListener(() => ActiveCurrentShop(ShopsType.bonusShop));
        _tricksBtn.onClick.AddListener(() => ActiveCurrentShop(ShopsType.tricksShop));

    }

    private void ActiveCurrentShop(ShopsType shop)
    {
        switch (shop)
        {
            case ShopsType.donatShop: _shopsBody.GetComponent<Image>().color = _donatBtn.gameObject.GetComponent<Image>().color;
                ActiveThisShop(_allShops[(int)ShopsType.donatShop]);
                break;
            case ShopsType.bonusShop: _shopsBody.GetComponent<Image>().color = _bonusesBtn.gameObject.GetComponent<Image>().color;
                ActiveThisShop(_allShops[(int)ShopsType.bonusShop]);
                break;
            case ShopsType.tricksShop:_shopsBody.GetComponent<Image>().color= _tricksBtn.gameObject.GetComponent<Image>().color;
                ActiveThisShop(_allShops[(int)ShopsType.tricksShop]);
                break;
            default:
                break;
        }

        _audio.PlaySound(Sounds.ShopSlot);
    }

    private void ActiveThisShop(GameObject shop)
    {
        for(int i =0; i < _allShops.Length; i++)
        {
            if (_allShops[i] == shop)
            {
                _allShops[i].SetActive(true);
            }
            else
            {
                _allShops[i].SetActive(false);
            }
        }
    }
}
