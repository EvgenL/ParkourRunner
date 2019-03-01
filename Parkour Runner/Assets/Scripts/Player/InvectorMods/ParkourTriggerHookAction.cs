using Basic_Locomotion.Scripts.CharacterController;
using Basic_Locomotion.Scripts.CharacterController.Actions;
using UnityEngine;

namespace ParkourRunner.Scripts.Player.InvectorMods
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
