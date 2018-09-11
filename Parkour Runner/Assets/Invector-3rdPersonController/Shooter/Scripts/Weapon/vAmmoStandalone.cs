using System.Collections;
using Basic_Locomotion.Scripts.CharacterController.Actions;
using Basic_Locomotion.Scripts.Generic;
using Shooter.Scripts.Shooter;
using UnityEngine;

namespace Shooter.Scripts.Weapon
{
    [vClassHeader("vAmmoStandalone")]
    public class vAmmoStandalone : vTriggerGenericAction
    {
        [Header("Ammo Standalone Options")]
        [Tooltip("Use the same name as in the AmmoManager")]
        public string weaponName;
        public int ammoID;
        public int ammoAmount;
        private vAmmoManager ammoManager;

        public override IEnumerator OnDoActionDelay(GameObject cc)
        {
            yield return StartCoroutine(base.OnDoActionDelay(cc));

            ammoManager = cc.gameObject.GetComponent<vAmmoManager>();

       
            ammoManager.AddAmmo(weaponName,ammoID,ammoAmount);       
        }
    }
}