using UnityEngine;

[CreateAssetMenu(fileName = "New BonusShopData", menuName = "BonusShopData", order = 51)]
public class BonusesShopData : ScriptableObject
{
    [SerializeField] private Sprite _bonusIcon;
    [SerializeField] private BonusName _bonusKind;
    [SerializeField] private int[] _prices;
        
    public Sprite BonusesIcon { get { return _bonusIcon; } }

    public BonusName BonusKind { get { return _bonusKind; } }

    public int[] Prices { get { return _prices; } }
}
