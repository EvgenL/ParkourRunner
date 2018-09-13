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
                _oldJumpHeight = _player.jumpHeight;
                _player.jumpHeight = JumpHeight;
                _player.PlatformJump(JumpSpeed);
                Invoke("ResetJumpHeight", 0.5f); //Костыль. Если вызвать сразу, или через 0.1 сек, то высота прыжка не изменится
            }
        }

        private void ResetJumpHeight()
        {
            var _player = vThirdPersonController.instance;
            _player.jumpHeight = _oldJumpHeight;
        }


    }
}
