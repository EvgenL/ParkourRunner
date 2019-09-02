using System.Collections;
using ParkourRunner.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;
using AEngine;

public class PostMortemScreen : MonoBehaviour
{
    public GameObject ReviveScreen;
    public GameObject _rateMeWindow;
    [SerializeField] private Image _reviveProgressImg;
    public GameObject WatchAdButton;
    public Text ReviveForMoneyBtnTxt;

    public GameObject ResultsScreen;
    [SerializeField] private LocalizationComponent _distanceLocalization;
    [SerializeField] private LocalizationComponent _recordLocalization;
    [SerializeField] private LocalizationComponent _coinsLocalization;
    public Text CoinsText;
    public Text MetresText;
    public Text RecordText;
    
    public GameObject NewRecordText;

    public float TimeToRevive = 5f;

    private GameManager _gm;
    private AdManager _ad;
    private bool _alive = true;
    private bool _adSeen; //Игрок уже смотрел рекламу?
    private bool _stopTimer = false;
    private bool _isRateMeMode;

    private AudioManager _audio;

    private void Start()
    {
        _gm = GameManager.Instance;
        _ad = AdManager.Instance;
        _audio = AudioManager.Instance;

        _isRateMeMode = false;
    }

    public void Show()
    {
        _alive = false;

        int revivePrice = _gm.ReviveCost;

        if (Wallet.Instance.AllCoins >= revivePrice || AdManager.Instance.IsAvailable())
        {
            ReviveScreen.SetActive(true);
            ReviveForMoneyBtnTxt.text = revivePrice.ToString();
            
            if (/*!_adSeen && */AdManager.Instance.IsAvailable())
            {
                WatchAdButton.SetActive(true);
            }
            else
            {
                WatchAdButton.SetActive(false);
            }

            _audio.PlaySound(Sounds.Result);

            StartCoroutine(CountTimer());
        }
        else
        {
            ExitReviveScreen();
        }
    }

    public void WatchAd()
    {
        _ad.SkipAdInOrder();

        _stopTimer = true;
        _audio.PlaySound(Sounds.Tap);
        _ad.ShowAdvertising(AdFinishedCallback, AdSkippedCallback, AdSkippedCallback);
    }

    private void AdFinishedCallback()
    {
        //_adSeen = true;
        Revive();
        _stopTimer = false;
    }

    private void AdSkippedCallback()
    {
        _stopTimer = false;
    }

    private IEnumerator CountTimer()
    {
        float time = 0f;
        while (time < TimeToRevive)
        {
            if (_alive)
            {
                yield break;
            }

            if (!_stopTimer)
            time += Time.deltaTime;

            float mappedTime = Utility.MapValue(time, 0f, TimeToRevive, 1f, 0f);
            _reviveProgressImg.fillAmount = mappedTime;

            yield return null;
        }

        //CheckRateMe();
        ExitReviveScreen();
    }

    public void CheckRateMe()
    {
        EnvironmentController.CheckKeys();
        if (!PlayerPrefs.HasKey("WAS_RATE"))
        {
            PlayerPrefs.SetInt("WAS_RATE", 0);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.GetInt(EnvironmentController.TUTORIAL_KEY) != 1 && PlayerPrefs.GetInt(EnvironmentController.ENDLESS_KEY) != 1 && PlayerPrefs.GetInt("WAS_RATE") != 1)
        {
            if (PlayerPrefs.GetInt(EnvironmentController.LEVEL_KEY) == 3)
            {
                PlayerPrefs.SetInt("WAS_RATE", 1);
                ShowRateMe();

                _audio.PlaySound(Sounds.Result);
            }
            else
                ExitReviveScreen();

            PlayerPrefs.Save();
        }
        else
            ExitReviveScreen();
    }

    public void Revive()
    {
        _alive = true;
        ReviveScreen.SetActive(false);
        ResultsScreen.SetActive(false);
        _gm.Revive();
    }

    public void ShowRateMe()
    {
        ReviveScreen.SetActive(false);
        ResultsScreen.SetActive(false);

        _rateMeWindow.SetActive(true);
    }

    public void ExitReviveScreen()
    {
        Wallet.Instance.Save();

        ReviveScreen.SetActive(false);
        ResultsScreen.SetActive(true);

        _audio.PlaySound(Sounds.ResultFull);

        MetresText.text = string.Format("{0}  {1} m", _distanceLocalization.Text, (int)_gm.DistanceRun);

        NewRecordText.SetActive(ProgressManager.IsNewRecord(_gm.DistanceRun));

        RecordText.text = string.Format("{0}  {1} m", _recordLocalization.Text, (int)ProgressManager.DistanceRecord);
        CoinsText.text = string.Format("{0}  {1}", _coinsLocalization.Text, Wallet.Instance.InGameCoins); //"Coins: " + Wallet.Instance.InGameCoins;

        if (_ad.CheckAdvertisingOrder())
            _ad.ShowAdvertising(null, null, null);
    }

    public void ReviveForMoney()
    {
        if (Wallet.Instance.SpendCoins(_gm.ReviveCost))
        {
            Revive();
            _audio.PlaySound(Sounds.Tap);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus && _isRateMeMode)
        {
            _isRateMeMode = false;
            _rateMeWindow.SetActive(false);
            ExitReviveScreen();
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause && _isRateMeMode)
        {
            _isRateMeMode = false;
            _rateMeWindow.SetActive(false);
            ExitReviveScreen();
        }
    }

    public void OnRateButtonClick()
    {
        AudioManager.Instance.PlaySound(Sounds.Tap);

        _isRateMeMode = true;
        Application.OpenURL(StaticConst.IOS_URL);
    }

    public void OnSkipRateButtonClick()
    {
        AudioManager.Instance.PlaySound(Sounds.Tap);

        _rateMeWindow.SetActive(false);
        ExitReviveScreen();
    }
}