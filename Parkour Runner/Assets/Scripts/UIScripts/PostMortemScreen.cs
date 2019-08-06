using System.Collections;
using ParkourRunner.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;
using AEngine;

public class PostMortemScreen : MonoBehaviour
{
    public GameObject ReviveScreen;
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

    private AudioManager _audio;

    private void Start()
    {
        _gm = GameManager.Instance;
        _ad = AdManager.Instance;
        _audio = AudioManager.Instance;
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

        ExitReviveScreen();
    }

    public void Revive()
    {
        _alive = true;
        ReviveScreen.SetActive(false);
        ResultsScreen.SetActive(false);
        _gm.Revive();
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
}