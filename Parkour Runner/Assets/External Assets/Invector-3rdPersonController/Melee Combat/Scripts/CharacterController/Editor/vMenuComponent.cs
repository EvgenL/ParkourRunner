using Basic_Locomotion.Scripts.CharacterController;
using UnityEngine;
using UnityEditor;
using Invector;
using ItemManager.Scripts;
using Melee_Combat.Scripts.LockOn;
using Melee_Combat.Scripts.MeleeWeapon;

// MELEE COMBAT FEATURES
public partial class vMenuComponent
{
    [MenuItem("Invector/Melee Combat/Components/MeleeManager")]
    static void MeleeManagerMenu()
    {
        if (Selection.activeGameObject)
            Selection.activeGameObject.AddComponent<vMeleeManager>();
        else
            Debug.Log("Please select a vCharacter to add the component.");
    }

    [MenuItem("Invector/Melee Combat/Components/WeaponHolderManager (Player Only)")]
    static void WeaponHolderMenu()
    {
        if (Selection.activeGameObject && Selection.activeGameObject.GetComponent<vThirdPersonInput>()!=null)
            Selection.activeGameObject.AddComponent<vWeaponHolderManager>();
        else
            Debug.Log("Please select the Player to add the component.");
    }
    [MenuItem("Invector/Melee Combat/Components/LockOn (Player Only)")]
    static void LockOnMenu()
    {
        if (Selection.activeGameObject && Selection.activeGameObject.GetComponent<vThirdPersonInput>() != null)
            Selection.activeGameObject.AddComponent<vLockOn>();
        else
            Debug.Log("Please select a Player to add the component.");
    }
}