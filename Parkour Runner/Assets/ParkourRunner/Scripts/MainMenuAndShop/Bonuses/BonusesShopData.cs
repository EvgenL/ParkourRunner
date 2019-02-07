using UnityEngine;

[CreateAssetMenu(fileName = "New BonusShopData", menuName = "BonusShopData", order = 51)]
public class BonusesShopData : ScriptableObject
{
    [SerializeField] private Sprite _bonusIcon;
    [SerializeField] private BonusName _bonusKind;
    [SerializeField] private int[] _prices;

    [Header("Old")]
    [SerializeField] private string _bonusName;
    [SerializeField] private float _payForThis;

    public Sprite BonusesIcon { get { return _bonusIcon; } }

    public BonusName BonusKind { get { return _bonusKind; } }

    public int[] Prices { get { return _prices; } }

    // OLD
    public string BonusesName { get { return _bonusName; } }
    public string PayForThis
    {
        get
        {
            return (_payForThis + Mathf.FloorToInt(PlayerPrefs.GetInt(_bonusName) * _payForThis/2)).ToString();
        }
        set
        {
            _payForThis = System.Convert.ToInt32(value);
        }
    }
}
