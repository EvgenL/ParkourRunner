
using Assets.Scripts.Pick_Ups;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    class ProgressManager : MonoBehaviour
    {
        #region Singleton

        public static ProgressManager Instance;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        #endregion

        public float MagnetCoinVelocity = 1f;
        public float MagnetRadius = 4f;
        public float InitialMagnetLength = 5f;
        public float MagnetLength = 0f;

        public float InitialJumpBonusHeight = 12f;
        public float JumpBonusHeight = 0f;
        public float InitialJumpLength = 5f;
        public float JumpLength = 0f;

        public float InitialShieldStrength = 100f;
        public float ShieldStrength = 0f;
        public float InitialShieldLength = 5f;
        public float ShieldLength = 0f;

        public float GetBonusLength(BonusPickUp bonusPickUp)
        {
            /*if (bonusPickUp is Bonus)
            {
                return MagnetLength;
            }*/

            return 0f;
        }
    }

}
