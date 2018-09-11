using System.Collections.Generic;
using UnityEngine;

namespace ItemManager.Scripts
{
    public class vItemListData : ScriptableObject
    {
        public List<vItem> items = new List<vItem>();       
       
        public bool inEdition;
       
        public bool itemsHidden = true;
        
    }

}
