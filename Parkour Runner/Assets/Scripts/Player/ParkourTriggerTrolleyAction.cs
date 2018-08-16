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

        public float StartDelay = 0.75f;

        public Vector3 PlayerOffset;
        
        private ParkourThirdPersonController _player;

        private void Start()
        {
            OnDoAction.AddListener(StartSlide);
            Path.Waypoints[1].reached.AddListener(JumpOff);
            _player = vThirdPersonController.instance.GetComponent<ParkourThirdPersonController>();
        }

        public void StartSlide()
        {
            Invoke("Play", StartDelay);
        }

        private void Play()
        {
            Path.Rewind();
            Path.Play();
        }

        private void JumpOff()
        {
            _player.IsSlidingTrolley = false;
        }
    }
}
