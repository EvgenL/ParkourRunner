using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Player.InvectorMods;
using ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using UnityEngine;

namespace Assets.ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    public abstract class Bonus : MonoBehaviour
    {
        private float TimeRemaining;
        protected ParkourThirdPersonController _player;
        protected ProgressManager _pm;
        private HUDManager _hud;
        [SerializeField] private BonusName _BonusName;

        private void Start()
        {
            _pm = ProgressManager.Instance;
            _player = ParkourThirdPersonController.instance;
            _hud = HUDManager.Instance;
            _hud.DisableBonus(_BonusName);
            //TODO play effect animation
        }

        public void RefreshTime()
        {
            TimeRemaining = ProgressManager.GetBonusLen(_BonusName);
            HUDManager.Instance.UpdateBonus(_BonusName, 1f);
        }

        void Update()
        {
            if (_hud == null) _hud = HUDManager.Instance;
            TimeRemaining -= Time.deltaTime;
            if (TimeRemaining <= 0f)
            {
                EndEffect();
                _hud.DisableBonus(_BonusName);
                enabled = false;
                return;
            }

            float percent = TimeRemaining / ProgressManager.GetBonusLen(_BonusName);
            _hud.UpdateBonus(_BonusName, percent);

            UpdateEffect(TimeRemaining);
        }

        protected abstract void EndEffect();
        protected abstract void UpdateEffect(float timeRemaining);
    }
}
