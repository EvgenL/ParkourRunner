
using Assets.Scripts.Pick_Ups;
using Assets.Scripts.Pick_Ups.Effects;
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

        public float MagnetRadius = 4f;     //Изначальный радиус магнита
        public float InitialMagnetLength = 5f;  //Изначальная длительность
        public float MagnetUpgradeLength = 0f;  //Продаваемый бонус к длительности

        public float InitialJumpBonusHeight = 12f; //Изначальная высота бонус-прыжка
        public float JumpUpgradeHeight = 0f;    //Апгрейд бонус-прыжка (нужно ли это? вынес параметр на всякий случай)
        public float InitialJumpLength = 5f;  //Изначальная длительность
        public float JumpUpgradeLength = 0f;  //Продаваемый бонус к длительности

        public float InitialShieldLength = 5f;  //Изначальная длительность
        public float ShieldUpgradeLength = 0f;  //Продаваемый бонус к длительности

    }
}
