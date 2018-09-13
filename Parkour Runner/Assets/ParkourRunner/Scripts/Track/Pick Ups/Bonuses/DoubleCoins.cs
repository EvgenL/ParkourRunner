using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basic_Locomotion.Scripts.CharacterController;
using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using UnityEngine;

namespace Assets.ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    class DoubleCoins : MonoBehaviour
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
            //TODO play effect animation
        }

        public void RefreshTime()
        {
            GameManager.Instance.CoinMultipiler = 2;
            TimeRemaining = _pm.InitialDoubleCoinsLength + _pm.DoubleCoinsUpgradeLength;
            HUDManager.Instance.UpdateBonus(BonusName.DoubleCoins, 1f);
        }


        void Update()
        {
            TimeRemaining -= Time.deltaTime;
            if (TimeRemaining <= 0f)
            {
                GameManager.Instance.CoinMultipiler = 1;
                HUDManager.Instance.DisableBonus(BonusName.DoubleCoins);
                Destroy(this);
                return;
            }

            float percent = TimeRemaining / (_pm.InitialDoubleCoinsLength + _pm.DoubleCoinsUpgradeLength);
            HUDManager.Instance.UpdateBonus(BonusName.DoubleCoins, percent);
        }
    }
}
