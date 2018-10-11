using Basic_Locomotion.Scripts.CharacterController;
using Basic_Locomotion.Scripts.CharacterController.Actions;
using ParkourRunner.Scripts.Managers;
using UnityEngine;

namespace ParkourRunner.Scripts.Player.InvectorMods
{
    class ParkourTriggerWallRunAction : vTriggerGenericAction
    {
        public Transform TargetTransform;
        public PathMagic.Scripts.PathMagic Path;

        public Vector3 PlayerOffset;
        public bool IsLeft;

        private ParkourThirdPersonController _player;

        private GameObject _arrowTutorialPrefab;

        private void Start()
        {
            base.Start();
            OnDoAction.AddListener(Play);
            Path.Waypoints[1].reached.AddListener(JumpOff);
            //TODO script exec order
            //_player = vThirdPersonController.instance.transform.GetComponent<ParkourThirdPersonController>();
            //_player = vThirdPersonController.instance.GetComponent<ParkourThirdPersonController>();

            _arrowTutorialPrefab = Resources.Load<GameObject>("Tutorial/ArrowUp");
            var arrowGo = Instantiate(_arrowTutorialPrefab, transform.position + transform.up + -transform.forward,
                Quaternion.identity);

            Destroy(arrowGo, 15f);
        }

        private void Update()
        {

        }

        private void Play()
        {
            if (IsLeft)
            {
                ParkourCamera.Instance.SwitchSides(true);
            }
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
            if (IsLeft)
            {
                ParkourCamera.Instance.SwitchSides(false);
            }
            if (_player.IsRunningWall)
            {
                GameManager.Instance.PlayerCanBeDismembered = true;
                _player.IsRunningWall = false;

                //_player.animator.SetTrigger("JumpOffWallTrigger");
                _player._capsuleCollider.isTrigger = false;
                _player._rigidbody.useGravity = true; 
            }
            Path.Rewind();
        }
    }
}
