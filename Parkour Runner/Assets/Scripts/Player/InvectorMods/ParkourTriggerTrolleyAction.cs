using Basic_Locomotion.Scripts.CharacterController;
using Basic_Locomotion.Scripts.CharacterController.Actions;
using UnityEngine;

namespace ParkourRunner.Scripts.Player.InvectorMods
{
    class ParkourTriggerTrolleyAction : vTriggerGenericAction
    {
        public Transform SliderTransform;
        public Transform HoldPointTransform;

        public PathMagic.Scripts.PathMagic Path;

        public Vector3 PlayerOffset;
        public float StartSlideDelay;

        private ParkourThirdPersonController _player;


        private void Start()
        {
            OnDoAction.AddListener(Play);
            Path.Waypoints[1].reached.AddListener(JumpOff);
            //Ну и с какого хуя нулл референс?
            //_player = vThirdPersonController.instance.GetComponent<ParkourThirdPersonController>();
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
            _player = vThirdPersonController.instance.GetComponent<ParkourThirdPersonController>();
            if (_player.IsSlidingTrolley)
            {
                _player.IsSlidingTrolley = false;
                _player._capsuleCollider.isTrigger = false; //
                _player._rigidbody.useGravity = true; //
            }
        }
    }
}
