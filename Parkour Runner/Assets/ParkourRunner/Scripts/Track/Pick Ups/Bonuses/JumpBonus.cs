using Basic_Locomotion.Scripts.CharacterController;
using ParkourRunner.Scripts.Managers;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
        class JumpBonus : MonoBehaviour
        {
            public float TimeRemaining;

            private vThirdPersonController _player;
            private ProgressManager _pm;

            private float _oldJumpHeight;

            void Start()
            {
                _pm = ProgressManager.Instance;

                _player = vThirdPersonController.instance;
                RefreshTime();

                _oldJumpHeight = _player.jumpHeight;
                _player.jumpHeight = _pm.InitialJumpBonusHeight + _pm.JumpUpgradeHeight;

                //TODO play effect animation
            }

            public void RefreshTime()
            {
                TimeRemaining = _pm.InitialJumpLength + _pm.JumpUpgradeLength;
                HUDManager.Instance.UpdateBonus(BonusName.Jump, 1f);
        }


            void Update()
            {
                TimeRemaining -= Time.deltaTime;
                if (TimeRemaining <= 0f)
                {
                    _player.jumpHeight = _oldJumpHeight;
                    HUDManager.Instance.DisableBonus(BonusName.Jump);
                    Destroy(this);
                    return;
                }

                float percent = TimeRemaining / (_pm.InitialMagnetLength + _pm.MagnetUpgradeLength);
                HUDManager.Instance.UpdateBonus(BonusName.Jump, percent);
            }
        }
}
