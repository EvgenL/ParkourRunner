using ParkourRunner.Scripts.Managers;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    class ShieldBonus : MonoBehaviour
    {
        public float TimeRemaining;
        
        private ProgressManager _pm;
        
        void Start()
        {
            _pm = ProgressManager.Instance;
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
            GameManager.Instance.PlayerCanBeDismembered = false;
            TimeRemaining -= Time.deltaTime;
            if (TimeRemaining <= 0f)
            {
                GameManager.Instance.PlayerCanBeDismembered = true;
                HUDManager.Instance.DisableBonus(BonusName.Shield);
                Destroy(this);
                return;
            }

            float percent = TimeRemaining / (_pm.InitialMagnetLength + _pm.MagnetUpgradeLength);
            HUDManager.Instance.UpdateBonus(BonusName.Shield, percent);
        }
    }
}
