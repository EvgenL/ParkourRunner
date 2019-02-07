using System.Collections;
using ParkourRunner.Scripts.Managers;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using AEngine;

public class PostMortemScreen : MonoBehaviour
{

    public GameObject ReviveScreen;
    public Slider ReviveScreenTimer;
    public GameObject WatchAdButton;
    public Text ReviveForMoneyBtnTxt;

    public GameObject ResultsScreen;
    public Text CoinsText;
    public Text MetresText;
    public Text RecordText;
    public GameObject NewRecordText;

    public float TimeToRevive = 5f;

    private GameManager _gm;
    private bool _alive = true;
    private bool _adSeen; //Игрок уже смотрел рекламу?
    private bool _stopTimer = false;

    private AudioManager _audio;

    private void Start()
    {
        _gm = GameManager.Instance;
        _audio = AudioManager.Instance;
    }

    public void Show()
    {
        _alive = false;
        ReviveScreen.SetActive(true);
        ReviveForMoneyBtnTxt.text = _gm.ReviveCost.ToString(); //TODO 0 cost bug
        if (!_adSeen && Advertisement.IsReady())
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

    public void WatchAd()
    {
        _stopTimer = true;
        _audio.PlaySound(Sounds.Tap);
        AdManager.Instance.ShowVideo(AdFinishedCallback, AdSkippedCallback);
    }

    private void AdFinishedCallback()
    {
        _adSeen = true;
        Revive();
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
            ReviveScreenTimer.value = mappedTime;

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
        ReviveScreen.SetActive(false);
        ResultsScreen.SetActive(true);

        _audio.PlaySound(Sounds.ResultFull);

        MetresText.text = "You run " + (int)_gm.DistanceRun + "m";

        NewRecordText.SetActive(ProgressManager.IsNewRecord(_gm.DistanceRun));

        RecordText.text = "Best: " + ProgressManager.DistanceRecord + "m";
        CoinsText.text = "Conis: " + Wallet.Instance.InGameCoins;
    }

    public void ReviveForMoney()
    {
        //if (ProgressManager.SpendCoins(_gm.ReviveCost)) 
        if (Wallet.Instance.SpendCoins(_gm.ReviveCost))
        {
            Revive();
            _audio.PlaySound(Sounds.Tap);
        }
        else //Если нам не хватило денег
        {
            //TODO Ожидание доната
        }
    }

}
