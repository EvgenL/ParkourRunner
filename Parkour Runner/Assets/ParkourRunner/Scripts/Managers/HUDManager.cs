using ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using ParkourRunner.Scripts.UIScripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace ParkourRunner.Scripts.Managers
{
    public class HUDManager : MonoBehaviour
    {
        public enum Messages
        {
            Ok,
            Great,
            Perfect
        }

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
        public PostMortemScreen PostMortemScreen;

        public Animator MetersRunAnimator;
        public Text MetersRunText;
        public Animator GreatTextAnimator;
        public Text GreatText;
        public Animator TrickNameTextAnimator;//TODO
        public Text TrickNameText;
        public Animator PauseAnimator;
        public GameObject PauseGo;

        public int ShowDistanceEvery = 500;
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

                MetersRunAnimator.enabled = true;
                MetersRunAnimator.Play("FadeIN_OUT");
            }
        }

        public void ShowGreatMessage(Messages msg)
        {
            string message = "";
            switch (msg)
            {
                case Messages.Ok:
                    message = "OK";
                    break;
                case Messages.Great:
                    message = "GREAT!";
                    break;
                case Messages.Perfect:
                    message = "PERFECT!!!";
                    break;
            }

            GreatText.text = message;
            GreatTextAnimator.enabled = true;
            GreatTextAnimator.Play("FadeIN_OUT");
        }

        public void ShowPostMortem()
        {
            PostMortemScreen.Show();
        }

        public void TogglePause()
        {
            if (GameManager.Instance.gameState == GameManager.GameState.Pause)
            {
                HidePause();
            }
            else
            {
                ShowPause();
            }
        }

        public void ShowPause()
        {
            PauseGo.SetActive(true);
            PauseAnimator.enabled = true;
            PauseAnimator.SetBool("IsDisplayed", true);
            GameManager.Instance.Pause();
        }

        public void HidePause()
        {
            GameManager.Instance.UnPause();
            PauseAnimator.SetBool("IsDisplayed", false);
        }
    }
}
