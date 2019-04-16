using UnityEngine;
using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Player.InvectorMods;

public class FinishMessage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<ParkourThirdPersonController>();

        if (target != null)
            HUDManager.Instance.ShowGreatMessage(HUDManager.Messages.LevelComplete);
    }
}