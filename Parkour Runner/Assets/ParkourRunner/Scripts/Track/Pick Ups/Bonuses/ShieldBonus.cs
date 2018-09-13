using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Player.InvectorMods;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    class ShieldBonus : MonoBehaviour
    {
        public float TimeRemaining;
        
        private ProgressManager _pm;

        private ParkourThirdPersonController _player;

        void Start()
        {
            _pm = ProgressManager.Instance;
            _player = ParkourThirdPersonController.instance;
            GameManager.Instance.PlayerCanBeDismembered = false;
            RefreshTime();

            //TODO play effect animation
        }

        public void RefreshTime()
        {
            TimeRemaining = _pm.InitialShieldLength + _pm.ShieldUpgradeLength;
            HUDManager.Instance.UpdateBonus(BonusName.Shield, 1f);
        }


        void Update()
        {
            _player.Immune = true;
            TimeRemaining -= Time.deltaTime;
            if (TimeRemaining <= 0f)
            {
                _player.Immune = false;
                HUDManager.Instance.DisableBonus(BonusName.Shield);
                Destroy(this);
                return;
            }

            float percent = TimeRemaining / (_pm.InitialMagnetLength + _pm.MagnetUpgradeLength);
            HUDManager.Instance.UpdateBonus(BonusName.Shield, percent);
        }
    }
}
