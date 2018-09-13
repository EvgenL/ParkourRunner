using System.Collections.Generic;
using System.Linq;
using ParkourRunner.Scripts.Player;
using ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using UnityEngine;

namespace ParkourRunner.Scripts.Managers
{
    public class ProgressManager : MonoBehaviour
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


        public const float InitialMagnetLength = 5f;  //Изначальная длительность
        public static float MagnetUpgradeLength = 0f;  //Продаваемый бонус к длительности

        public static float InitialJumpLength = 10f;  //Изначальная длительность
        public static float JumpUpgradeLength = 0f;  //Продаваемый бонус к длительности

        public const float InitialShieldLength = 10f;  //Изначальная длительность
        public static float ShieldUpgradeLength = 0f;  //Продаваемый бонус к длительности

        public const float InitialBoostLength = 10f;  //Изначальная длительность
        public static float BoostUpgradeLength = 0f;  //Продаваемый бонус к длительности

        public const float InitialDoubleCoinsLength = 10f;  //Изначальная длительность
        public static float DoubleCoinsUpgradeLength = 0f;  //Продаваемый бонус к длительности

        public static int GameLaunches; //Сколько раз запущена игра
        public static float DistanceRecord { get; private set; } //Рекорд игрока
        public static float Coins { get; private set; } //Деньги игрока

        public static float GetBonusLen(BonusName bonusName)
        {
            switch (bonusName)
            {
                case (BonusName.Magnet):
                    return InitialMagnetLength + MagnetUpgradeLength;
                case (BonusName.Jump):
                    return InitialJumpLength + JumpUpgradeLength;
                case (BonusName.Shield):
                    return InitialShieldLength + ShieldUpgradeLength;
                case (BonusName.DoubleCoins):
                    return InitialDoubleCoinsLength + DoubleCoinsUpgradeLength;
                case (BonusName.Boost):
                    return InitialBoostLength + BoostUpgradeLength;
            }

            return 0;
        }

        private void Start()
        {
            LoadSettings();
            GameLaunch();
        }

        private static void GameLaunch()
        {
            if (GameLaunches == 0)
            {
                //TODO
            }

            GameLaunches++;
        }

        private void OnDisable()
        {
            SaveSettings();
        }


        public List<Trick> GetBoughtJumpOverTricks()
        {
            return ResourcesManager.Instance.JumpOverTricks.Where(x => x.IsBought).ToList();
        }

        public Trick GetRandomJumpOver()
        {
            var tricks = GetBoughtJumpOverTricks();
            int randomIndex = UnityEngine.Random.Range(0, tricks.Count);
            if (randomIndex < 0) return null;

            return tricks[randomIndex];
        }



        public static void LoadSettings()
        {
            GameLaunches = PlayerPrefs.GetInt("GameLaunches", 0);
            Coins = PlayerPrefs.GetFloat("Coins", 0);
            DistanceRecord = PlayerPrefs.GetFloat("DistanceRecord", 0);
        }

        public static void SaveSettings()
        {
            PlayerPrefs.SetInt("GameLaunches", GameLaunches);
            PlayerPrefs.SetFloat("Coins", Coins);
            PlayerPrefs.SetFloat("DistanceRecord", DistanceRecord);
        }

        public static void ResetSettings()
        {
            PlayerPrefs.SetFloat("Coins", 0);
            PlayerPrefs.SetFloat("DistanceRecord", 0);
            //ResetTrickPurchases(); //TODO
        }

        public static bool IsNewRecord(float metres)
        {
            if (metres > DistanceRecord)
            {
                DistanceRecord = metres;
                return true;
            }
            return false;
        }

        public static void AddCoin(int value = 1)
        {
            Coins += value;
        }

        public static bool SpendCoins(int value)
        {
            if (Coins - value >= 0)
            {
                Coins -= value;
                return true;
            }
            return false;
        }
    }
}
