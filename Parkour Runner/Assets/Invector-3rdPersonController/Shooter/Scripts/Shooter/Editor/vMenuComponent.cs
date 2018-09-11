using UnityEngine;
using System.Collections;
using Basic_Locomotion.Scripts.CharacterController;
using Shooter.Scripts.Shooter;
using UnityEditor;
public partial class vMenuComponent
{
    [MenuItem("Invector/Shooter/Components/LockOn (Player Shooter Only)")]
    static void LockOnShooterMenu()
    {
        if (Selection.activeGameObject && Selection.activeGameObject.GetComponent<vThirdPersonInput>() != null)
            Selection.activeGameObject.AddComponent<vLockOnShooter>();
        else
            Debug.Log("Please select a Player to add the component.");
    }
}
