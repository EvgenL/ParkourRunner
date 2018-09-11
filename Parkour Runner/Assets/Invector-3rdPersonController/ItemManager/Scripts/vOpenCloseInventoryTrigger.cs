using Basic_Locomotion.Scripts.Generic;

namespace ItemManager.Scripts
{
    [vClassHeader("vOpenClose Inventory Trigger", false)]
    public class vOpenCloseInventoryTrigger : vMonoBehaviour
    {

        public UnityEngine.Events.UnityEvent onOpen, onClose;
        protected virtual void Start()
        {
            var inventory = GetComponentInParent<vInventory>();
            if (inventory) inventory.onOpenCloseInventory.AddListener(OpenCloseInventory);
        }
        public void OpenCloseInventory(bool value)
        {
            if (value) onOpen.Invoke();
            else onClose.Invoke();
        }
    }
}

