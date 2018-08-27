using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Player.InvectorMods;
using Invector.CharacterController;
using Jacovone;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class ParkourTriggerTrolleyAction : vTriggerGenericAction
    {
        public Transform SliderTransform;
        public Transform HoldPointTransform;

        public PathMagic Path;

        public Vector3 PlayerOffset;
        public float StartSlideDelay;

        private ParkourThirdPersonController _player;


        private void Start()
        {
            OnDoAction.AddListener(Play);
            Path.Waypoints[1].reached.AddListener(JumpOff);
            _player = vThirdPersonController.instance.GetComponent<ParkourThirdPersonController>();
        }

        private void Play()
        {
            Path.Rewind();
            Invoke("StartSlide", StartSlideDelay);
        }

        private void StartSlide()
        {
            Path.Play();
        }

        private void JumpOff()
        {
            if (_player.IsSlidingTrolley)
            {
                _player.IsSlidingTrolley = false;
                _player._capsuleCollider.isTrigger = true;
                _player._rigidbody.useGravity = false;
            }
        }
    }
}
