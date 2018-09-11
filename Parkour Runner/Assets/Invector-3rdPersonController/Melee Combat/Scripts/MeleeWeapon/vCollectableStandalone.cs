using System.Collections;
using Basic_Locomotion.Scripts.CharacterController.Actions;
using Basic_Locomotion.Scripts.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Melee_Combat.Scripts.MeleeWeapon
{
    [vClassHeader("vCollectableStandalone", false)]
    public class vCollectableStandalone : vTriggerGenericAction
    {
        public string targetEquipPoint;
        public GameObject weapon;
        public Sprite weaponIcon;
        public string weaponText;
        public UnityEvent OnEquip;
        public UnityEvent OnDrop;
  
        private vCollectMeleeControl manager;   

        public override IEnumerator OnDoActionDelay(GameObject cc)
        {
            yield return StartCoroutine(base.OnDoActionDelay(cc));

            manager = cc.GetComponent<vCollectMeleeControl>();

            if (manager != null)
            {
                manager.HandleCollectableInput(this);
            }
        }
    }
}