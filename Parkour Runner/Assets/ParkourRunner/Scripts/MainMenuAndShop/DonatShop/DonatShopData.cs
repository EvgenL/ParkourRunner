using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New DonatShopData", menuName = "DonatShopData", order = 52)]
public class DonatShopData : ScriptableObject

{
    [SerializeField] private string _donatName;
    [SerializeField] private Sprite _donatIcon;
    
    public string DonatsName
    {
        get
        {
            return _donatName;
        }
    }
    public Sprite DonatsIcon
    {
        get
        {
            return _donatIcon;
        }
    }

}
