using ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using ParkourRunner.Scripts.UIScripts;
using UnityEngine;
using UnityEngine.UI;

namespace ParkourRunner.Scripts.Managers
{
    public class HUDManager : MonoBehaviour
    {

        public static HUDManager Instance;

        #region Singleton

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this);
            }
        }

        #endregion

        public Image flashImage;
        public float FlashSpeed = 5f;

        public Text CoinsText;

        public BonusPanel BonusPanel;

        public int ShowDistanceEvery = 1000;
        private int _distanceShwonTimes;

        private bool _flashing = false;

        void Start()
        {
            SetCoins(0);
        }

        void Update()
        {
            ShowFlash();
        }

        public void Flash()
        {
            _flashing = true;
            flashImage.enabled = true;
        }

        private void ShowFlash()
        {
            if (!flashImage.enabled) return;

            if (_flashing)
            {
                _flashing = false;
                if (flashImage != null)
                    flashImage.color = Color.white; //Ставим непрозрачный цвет
            }
            else if (flashImage != null)
                flashImage.color = Color.Lerp(flashImage.color, Color.clear, FlashSpeed * Time.deltaTime);

            if (flashImage.color == Color.clear)
            {
                flashImage.enabled = false;
            }
        }

        public void SetCoins(int value)
        {
            if (value == 0)
                CoinsText.text = "";
            else
                CoinsText.text = value + "$";
        }

        public void UpdateBonus(BonusName bonusName, float value)
        {
            BonusPanel.UpdateBonus(bonusName, value);
        }

        public void DisableBonus(BonusName bonusName)
        {
            BonusPanel.DisableBonus(bonusName);
        }

        public void UpdateDistance(float value)
        {
            int times = (int)value / ShowDistanceEvery;
            if (times > _distanceShwonTimes)
            {
                _distanceShwonTimes = times;
                print("You ran " + times * ShowDistanceEvery + " meters! Todo ui for this message");
            }
        }
    }
}
