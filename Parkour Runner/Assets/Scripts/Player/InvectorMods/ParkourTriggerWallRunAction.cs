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
            //TODO script exec order
            //_player = vThirdPersonController.instance.transform.GetComponent<ParkourThirdPersonController>();
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
            GameManager.Instance.PlayerCanBeDismembered = false;

            _player._capsuleCollider.isTrigger = true; 
            _player._rigidbody.useGravity = false; 
            _player._rigidbody.velocity = Vector3.zero;

            _player.WallOffset = PlayerOffset;
            _player.TargetTransform = TargetTransform;
        }

        private void JumpOff()
        {
            if (_player.IsRunningWall)
            {
                GameManager.Instance.PlayerCanBeDismembered = true;
                _player.IsRunningWall = false;

                _player.animator.SetTrigger("JumpOffWallTrigger");
                _player._capsuleCollider.isTrigger = false;
                _player._rigidbody.useGravity = true; 
            }
        }
    }
}
