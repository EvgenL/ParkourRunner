﻿using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Player.InvectorMods;
using UnityEngine;

namespace Assets.ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    public abstract class Bonus : MonoBehaviour
    {
        [SerializeField] private BonusName _BonusName;

        private float _timeRemaining;
        protected ParkourThirdPersonController _player;
        protected ProgressManager _pm;
        private HUDManager _hud;

        private bool _active;
                
        private void Start()
        {
            _pm = ProgressManager.Instance;
            _player = ParkourThirdPersonController.instance;
            _hud = HUDManager.Instance;
        }

        private void OnDisable()
        {
            if (GameManager.Instance != null)
                GameManager.Instance.OnDie -= OnDie;
        }

        void Update()
        {
            if (!_active) return;

            if (_hud == null)
                _hud = HUDManager.Instance;

            _timeRemaining -= Time.deltaTime;

            if (_timeRemaining <= 0f)
            {
                _hud.UpdateBonus(_BonusName, 0f);
                End();
            }
            else
            {
                float percent = _timeRemaining / ProgressManager.Instance.GetBonusLen(_BonusName);
                _hud.UpdateBonus(_BonusName, percent);

                UpdateEffect(_timeRemaining);
            }           
        }

        public void RefreshTime()
        {
            _timeRemaining = ProgressManager.Instance.GetBonusLen(_BonusName);
            HUDManager.Instance.UpdateBonus(_BonusName, 1f);
            GameManager.Instance.OnDie += OnDie;
            _active = true;

            StartEffect();
        }

        private void End()
        {
            EndEffect();
            _hud.DisableBonus(_BonusName);
            GameManager.Instance.OnDie -= OnDie;
            _active = false;
        }

        protected virtual void StartEffect() {}

        protected virtual void EndEffect() {}

        protected virtual void UpdateEffect(float timeRemaining) {}

        #region Events
        private void OnDie()
        {
            _timeRemaining = 0f;
            _hud.UpdateBonus(_BonusName, 0f);
            End();
        }
        #endregion
    }
}
