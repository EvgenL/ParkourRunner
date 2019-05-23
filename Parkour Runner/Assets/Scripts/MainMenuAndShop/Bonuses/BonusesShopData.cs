using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New BonusShopData", menuName = "BonusShopData", order = 51)]
public class BonusesShopData : ScriptableObject
{
    [Serializable]
    public struct IconData
    {
        public Sprite icon;
        public int progressPower;
    }

    [SerializeField] private IconData[] _icons;
    [SerializeField] private BonusName _bonusKind;
    [SerializeField] private int[] _prices;
    
    public IconData[] Icons { get { return _icons; } }
    
    public BonusName BonusKind { get { return _bonusKind; } }

    public int[] Prices { get { return _prices; } }
}
