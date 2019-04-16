using UnityEngine;
using ParkourRunner.Scripts.Player.InvectorMods;

public class FinishCamera : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<ParkourThirdPersonController>();

        if (target != null)
        {
            ParkourCamera.Instance.LockCamera = true;

            var player = ParkourThirdPersonController.instance;
            var input = player.GetComponent<ParkourThirdPersonInput>();

            input.lockInput = true;
        }
    }
}