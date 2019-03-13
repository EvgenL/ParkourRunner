using System.Collections.Generic;
using ParkourRunner.Scripts.Player;
using UnityEngine;

namespace ParkourRunner.Scripts.Managers
{
    public class ProgressManager : MonoBehaviour
    {
        public static ProgressManager Instance;
        
        [SerializeField] private List<BonusData> _pickUps;
        
        public static int GameLaunches; //Сколько раз запущена игра
        public static float DistanceRecord { get; private set; } //Рекорд игрока
        
        

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            LoadSettings();
            GameLaunch();
        }

        public float GetBonusLen(BonusName bonusName)
        {
            BonusData data = null;
            foreach (BonusData item in _pickUps)
            {
                if (item.BonusKind == bonusName)
                {
                    data = item;
                    break;
                }
            }

            if (data != null)
            {
                int bonusLevel = PlayerPrefs.GetInt(bonusName.ToString());
                if (bonusLevel > data.DurationPowers.Length)
                {
                    Debug.LogError("Incorrect upgraded length, bonus level = " + bonusLevel);
                    bonusLevel = 0;
                }

                return (bonusLevel == 0) ? data.BaseDuration : data.BaseDuration + data.DurationPowers[bonusLevel - 1];
            }

            return 0;
        }

        private static void GameLaunch()
        {
            if (GameLaunches == 0)
            {
                //TODO при первом запуске показывать туториал
            }

            GameLaunches++;
        }

        private void OnDisable()
        {
            SaveSettings();
        }


        public List<Trick> GetBoughtJumpOverTricks()
        {
            return ResourcesManager.JumpOverTricks.FindAll(x => x.IsBought);
        }

        public Trick GetRandomJumpOver()
        {
            var tricks = GetBoughtJumpOverTricks();
            int randomIndex = UnityEngine.Random.Range(0, tricks.Count);
            if (randomIndex < 0) return null;

            return tricks[randomIndex];
        }

        public Trick GetRandomStand()
        {
            var tricks = ResourcesManager.StandTricks.FindAll(x => x.IsBought);
            int randomIndex = UnityEngine.Random.Range(0, tricks.Count);
            if (randomIndex < 0) return null;

            return tricks[randomIndex];
        }

        public Trick GetRandomRoll()
        {
            var tricks = ResourcesManager.RollTricks.FindAll(x => x.IsBought);
            int randomIndex = UnityEngine.Random.Range(0, tricks.Count);
            if (randomIndex < 0) return null;

            return tricks[randomIndex];
        }

        public Trick GetRandomSlide()
        {
            var tricks = ResourcesManager.SlideTricks.FindAll(x => x.IsBought);

            int randomIndex = UnityEngine.Random.Range(0, tricks.Count);
            if (randomIndex < 0) return null;

            return tricks[randomIndex];
        }


        public static void LoadSettings()
        {
            GameLaunches = PlayerPrefs.GetInt("GameLaunches", 0);
            DistanceRecord = PlayerPrefs.GetFloat("DistanceRecord", 0);
        }

        public static void SaveSettings()
        {
            PlayerPrefs.SetInt("GameLaunches", GameLaunches);
            PlayerPrefs.SetFloat("DistanceRecord", (int)DistanceRecord);
        }

        public static void ResetSettings()
        {
            PlayerPrefs.SetFloat("DistanceRecord", 0);
            //ResetTrickPurchases(); //TODO
        }

        /*private void ResetTrickPurchases()
        {
            var tricks = ResourcesManager.Instance.JumpOverTricks.ToList();
            foreach (var trick in tricks)
            {
                if (trick.name != "
            }
        }*/

        public static bool IsNewRecord(float metres)
        {
            if (metres > DistanceRecord)
            {
                DistanceRecord = metres;
                return true;
            }
            return false;
        }
    }
}
