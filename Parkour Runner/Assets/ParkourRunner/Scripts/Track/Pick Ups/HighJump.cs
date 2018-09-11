﻿using Basic_Locomotion.Scripts.CharacterController;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Pick_Ups
{
    public class HighJump : MonoBehaviour
    {

        public float JumpHeight = 12f;

        private float _oldJumpHeight;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                var _player = vThirdPersonController.instance;
                _oldJumpHeight = _player.jumpHeight;
                _player.jumpHeight = JumpHeight;
                _player.ForceJump();
                Invoke("Reset", 0.5f);
            }
        }

        private void Reset()
        {
            var _player = vThirdPersonController.instance;
            _player.jumpHeight = _oldJumpHeight;
        }


    }
}
