
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

        public static float MagnetCoinVelocity = 1f; //Скорость монеток, летящих к игроку (не трогать)
        public float MagnetRadius = 4f;     //Изначальный радиус магнита
        public float InitialMagnetLength = 5f;  //Изначальная длительность
        public float MagnetUpgradeLength = 0f;  //Продаваемый бонус к длительности

        public float InitialJumpBonusHeight = 12f; //Изначальная высота бонус-прыжка
        public float JumpUpgradeHeight = 0f;    //Апгрейд бонус-прыжка (нужно ли это? вынес параметр на всякий случай)
        public float InitialJumpLength = 5f;  //Изначальная длительность
        public float JumpUpgradeLength = 0f;  //Продаваемый бонус к длительности

        public float InitialShieldStrength = 100f; //Прочность конечностей со щитом
        public float ShieldUpgradeStrength = 0f;    //Продаваемый бонус к прочности
        public float InitialShieldLength = 5f;  //Изначальная длительность
        public float ShieldUpgradeLength = 0f;  //Продаваемый бонус к длительности

        public float GetBonusLength(BonusName name) //TODO
        {
            /*if (bonusPickUp is Bonus)
            {
                return MagnetLength;
            }*/

            return 0f;
        }
    }

}
