using System.Collections.Generic;
using ItemManager.Scripts.vItemEnumsBuilder;
using UnityEngine;
using UnityEngine.UI;

namespace ItemManager.Scripts
{
    public class vItemOptionWindow : MonoBehaviour
    {
        public Button useItemButton;
        public List<vItemType> itemsCanBeUsed = new List<vItemType>() { vItemType.Consumable };

        public virtual void EnableOptions(vItemSlot slot)
        {
            if (slot ==null || slot.item==null) return;
            useItemButton.interactable = itemsCanBeUsed.Contains(slot.item.type);
        }        
    }
}

