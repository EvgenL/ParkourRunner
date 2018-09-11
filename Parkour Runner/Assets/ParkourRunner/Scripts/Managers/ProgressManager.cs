using System.Collections.Generic;
using System.Linq;
using ParkourRunner.Scripts.Player;
using UnityEngine;

namespace ParkourRunner.Scripts.Managers
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


        public float MagnetRadius = 8f;     //Изначальный радиус магнита
        public float InitialMagnetLength = 5f;  //Изначальная длительность
        public float MagnetUpgradeLength = 0f;  //Продаваемый бонус к длительности

        public float InitialJumpBonusHeight = 12f; //Изначальная высота бонус-прыжка
        public float InitialJumpLength = 5f;  //Изначальная длительность
        public float JumpUpgradeLength = 0f;  //Продаваемый бонус к длительности

        public float InitialShieldLength = 5f;  //Изначальная длительность
        public float ShieldUpgradeLength = 0f;  //Продаваемый бонус к длительности

        public static int GameLaunches; //Сколько раз запущена игра
        public static float DistanceRecord { get; private set; } //Рекорд игрока
        public static float Coins { get; private set; } //Деньги игрока

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
