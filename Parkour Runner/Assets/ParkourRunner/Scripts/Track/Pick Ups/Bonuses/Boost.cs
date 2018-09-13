using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basic_Locomotion.Scripts.CharacterController;
using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Player.InvectorMods;
using ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using UnityEngine;

namespace Assets.ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    class Boost : MonoBehaviour
    {
        public float TimeRemaining;

        private ParkourThirdPersonController _player;
        private ProgressManager _pm;

        private float _oldJumpHeight;

        void Start()
        {
            _pm = ProgressManager.Instance;

            _player = ParkourThirdPersonController.instance;

            RefreshTime();

            _player.SpeedMult = 2f;
            _player.Immune = true;
            GameManager.Instance.PlayerCanBeDismembered = false;

            //TODO play effect animation
        }

        public void RefreshTime()
        {
            TimeRemaining = _pm.InitialBoostLength + _pm.BoostUpgradeLength;
            HUDManager.Instance.UpdateBonus(BonusName.Boost, 1f);
        }


        void Update()
        {
            TimeRemaining -= Time.deltaTime;
            _player.Immune = true;
            if (TimeRemaining <= 0f)
            {
                _player.SpeedMult = 1f;
                _player.Immune = false;
                HUDManager.Instance.DisableBonus(BonusName.Boost);
                Destroy(this);
                return;
            }

            float percent = TimeRemaining / (_pm.InitialBoostLength + _pm.BoostUpgradeLength);
            HUDManager.Instance.UpdateBonus(BonusName.Boost, percent);
        }
    }
}
