using Basic_Locomotion.Scripts.CharacterController;
using ParkourRunner.Scripts.Player.InvectorMods;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Pick_Ups
{
    public class JumpPlatform : MonoBehaviour
    {

        public float JumpHeight = 12f;
        public float JumpSpeed = 7f;

        private float _oldJumpHeight;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                ParkourThirdPersonController _player = ParkourThirdPersonController.instance;
                _player.PlatformJump(JumpSpeed, JumpHeight);
            }

            enabled = false;
            Invoke("Activate", 0.5f);
        }

        private void Activate()
        {
            enabled = true;
        }
    }
}
