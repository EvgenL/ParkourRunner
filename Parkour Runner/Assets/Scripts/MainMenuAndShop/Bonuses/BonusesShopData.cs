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
    [SerializeField] private string _description;
    
    public IconData[] Icons { get { return _icons; } }    
    public BonusName BonusKind { get { return _bonusKind; } }
    public int[] Prices { get { return _prices; } }
    public string Description { get { return _description; } }

    public Sprite GetActualIcon(int progress)
    {
        for (int i = 0; i < _icons.Length; i++)
        {
            if (progress <= _icons[i].progressPower)
                return _icons[i].icon;
        }

        return _icons[0].icon;
    }
}