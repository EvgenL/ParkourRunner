using ParkourRunner.Scripts.Player;
using ParkourRunner.Scripts.UIScripts;
using AEngine;
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
            Perfect,
            NoMessage,
            LevelComplete,
            CurrentLevel
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

            _wallet = Wallet.Instance;
            _audio = AudioManager.Instance;
        }

        #endregion

        public Image flashImage;
        public float FlashSpeed = 5f;
                
        public BonusPanel BonusPanel;
        public PostMortemScreen PostMortemScreen;

        public Animator MetersRunAnimator;
        public Text MetersRunText;
        public Animator GreatTextAnimator;
        public Text GreatText;
        public Animator TrickNameTextAnimator;
        public Text TrickNameText;
        public Animator RewardTextAnimator;
        public Text RewardText;
        public Animator PauseAnimator;
        public GameObject PauseGo;

        public int ShowDistanceEvery = 500;
        private int _distanceShownTimes;

        private bool _flashing = false;

        private Wallet _wallet;
        private AudioManager _audio;

        void Start()
        {
            _wallet.ResetInGameCoins();
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
            if (times > _distanceShownTimes)
            {
                _distanceShownTimes = times;

                MetersRunAnimator.enabled = true;
                MetersRunText.text = times * ShowDistanceEvery + " M";
                MetersRunAnimator.Play("FadeIN_OUT");
            }
        }

        public void ShowGreatMessage(Messages msg)
        {
            string message = "";
            switch (msg)
            {
                case Messages.NoMessage:
                    break;
                case Messages.Ok:
                    message = "OK";
                    break;
                case Messages.Great:
                    message = ParseLocalizationText("great", "GREAT!");
                    break;
                case Messages.Perfect:
                    message = ParseLocalizationText("perfect", "PERFECT!!!");
                    break;

                case Messages.LevelComplete:
                    message = ParseLocalizationText("level_complete", "LEVEL COMPLETE!!!");
                    break;

                case Messages.CurrentLevel:
                    if (PlayerPrefs.GetInt(EnvironmentController.ENDLESS_KEY) == 0 && PlayerPrefs.GetInt(EnvironmentController.TUTORIAL_KEY) == 0)
                        message = string.Format("{0} {1}", ParseLocalizationText("level", "Level"), PlayerPrefs.GetInt(EnvironmentController.LEVEL_KEY));
                    else
                        message = ParseLocalizationText("start", "Start");                    
                    break;
            }

            GreatText.text = message;
            GreatTextAnimator.enabled = true;
            GreatTextAnimator.Play("FadeIN_OUT");
        }

        private string ParseLocalizationText(string key, string defaultText = "")
        {
            if (!LocalizationManager.Instance.LockLocalization)
            {
                return LocalizationManager.Instance.GetText(key);
            }

            Debug.Log("Localization key " + key + "was not found or used debug mode");

            return defaultText;
        }

        public void Reward(int value)
        {
            RewardText.text = "+" + value;
            RewardTextAnimator.enabled = true;
            RewardTextAnimator.Play("Reward");
        }

        public void ShowTrickReward(Trick trick, float mult = 1)
        {
            GameManager.Instance.AddCoin();
            Reward((int)(trick.MoneyReward * mult));
        }

        public void ShowTrickName(Trick trick, float mult = 1)
        {
            string message = trick.Name + "!";

            TrickNameText.text = message;
            TrickNameTextAnimator.enabled = true;
            TrickNameTextAnimator.Play("FadeIN_OUT");
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

            _audio.PlaySound(Sounds.Tap);
            _audio.PlaySound(Sounds.WinSimple);
        }

        public void HidePause()
        {
            GameManager.Instance.UnPause();
            PauseAnimator.SetBool("IsDisplayed", false);

            _audio.PlaySound(Sounds.Tap);
        }
    }
}
