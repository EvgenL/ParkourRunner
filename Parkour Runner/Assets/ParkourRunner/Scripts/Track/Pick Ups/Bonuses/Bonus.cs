using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Player.InvectorMods;
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

        private bool _active;

        private void Start()
        {
            _pm = ProgressManager.Instance;
            _player = ParkourThirdPersonController.instance;
            _hud = HUDManager.Instance;
            //TODO play effect animation
        }

        public void RefreshTime()
        {
            TimeRemaining = ProgressManager.Instance.GetBonusLen(_BonusName);
            HUDManager.Instance.UpdateBonus(_BonusName, 1f);
            _active = true;
            StartEffect();
        }

        void Update()
        {
            if (!_active) return;

            if (_hud == null) _hud = HUDManager.Instance;
            TimeRemaining -= Time.deltaTime;
            if (TimeRemaining <= 0f)
            {
                End();
                return;
            }

            float percent = TimeRemaining / ProgressManager.Instance.GetBonusLen(_BonusName);
            _hud.UpdateBonus(_BonusName, percent);

            UpdateEffect(TimeRemaining);
        }

        private void End()
        {
            EndEffect();
            _hud.DisableBonus(_BonusName);
            _active = false;
        }

        protected virtual void StartEffect()
        {
        }

        protected virtual void EndEffect()
        {
        }

        protected virtual void UpdateEffect(float timeRemaining)
        {
        }
    }
}
