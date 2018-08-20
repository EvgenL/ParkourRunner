using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Invector.CharacterController;
using Jacovone;
using UnityEngine;

namespace Assets.Scripts.Player.InvectorMods
{
    class ParkourTriggerWallRunAction : vTriggerGenericAction
    {
        public Transform TargetTransform;
        public PathMagic Path;

        public Vector3 PlayerOffset;

        private ParkourThirdPersonController _player;

        private void Start()
        {
            base.Start();
            OnDoAction.AddListener(Play);
            Path.Waypoints[1].reached.AddListener(JumpOff);
            _player = vThirdPersonController.instance.GetComponent<ParkourThirdPersonController>();
            //_player = vThirdPersonController.instance.GetComponent<ParkourThirdPersonController>();
        }

        private void Update()
        {

        }

        private void Play()
        {
            _player = vThirdPersonController.instance.GetComponent<ParkourThirdPersonController>();
            Path.Rewind();
            Path.Play();
            _player.IsRunningWall = true;

            _player._capsuleCollider.isTrigger = true; // disable the collision of the player if necessary 
            _player._rigidbody.useGravity = false; // disable gravity of the player
            _player._rigidbody.velocity = Vector3.zero;

            _player.WallOffset = PlayerOffset;
            _player.TargetTransform = TargetTransform;
        }

        private void JumpOff()
        {
            _player.IsRunningWall = false;

            _player._capsuleCollider.isTrigger = false; // disable the collision of the player if necessary 
            _player._rigidbody.useGravity = true; // disable gravity of the player
        }
    }
}
