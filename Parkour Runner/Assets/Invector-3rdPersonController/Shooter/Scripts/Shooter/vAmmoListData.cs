using System.Collections.Generic;
using ItemManager.Scripts;
using UnityEngine;

namespace Shooter.Scripts.Shooter
{
    public class vAmmoListData : ScriptableObject
    {
        public List<vItemListData> itemListDatas;
        [HideInInspector]
        public List<vAmmo> ammos = new List<vAmmo>();  
    }
}
