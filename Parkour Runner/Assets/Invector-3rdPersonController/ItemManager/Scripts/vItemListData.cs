using UnityEngine;
using System.Collections.Generic;

namespace Invector.ItemManager
{
    public class vItemListData : ScriptableObject
    {
        public List<vItem> items = new List<vItem>();       
       
        public bool inEdition;
       
        public bool itemsHidden = true;
        
    }

}
