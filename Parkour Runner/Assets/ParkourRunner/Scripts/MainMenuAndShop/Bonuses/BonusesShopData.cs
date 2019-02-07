using UnityEngine;

[CreateAssetMenu(fileName = "New BonusShopData", menuName = "BonusShopData", order = 51)]
public class BonusesShopData : ScriptableObject
{
    [SerializeField] private string _bonusName;
    [SerializeField] private Sprite _bonusIcon;
    [SerializeField] private float _payForThis;
    
    

    public string BonusesName
    {
        get
        {
            return _bonusName;
        }
    }

    public Sprite BonusesIcon
    {
        get
        {
            return _bonusIcon;
        }
    }
    
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
