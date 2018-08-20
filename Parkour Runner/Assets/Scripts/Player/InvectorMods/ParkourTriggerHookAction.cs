using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Player.InvectorMods;
using Invector.CharacterController;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class ParkourTriggerHookAction : vTriggerGenericAction
    {
        public Transform HookTarget;
        public Vector3 PlayerOffset;

        
        private ParkourThirdPersonController _player;

        private void Start()
        {
            OnDoAction.AddListener(Play);
        }
        private void Play()
        {
            _player = vThirdPersonController.instance.GetComponent<ParkourThirdPersonController>();
            _player.IsUsingHook = true;
            _player.TargetTransform = HookTarget;
        }
    }
}
