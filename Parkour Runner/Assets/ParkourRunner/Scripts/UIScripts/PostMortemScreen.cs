using System.Collections;
using System.Collections.Generic;
using ParkourRunner.Scripts.Managers;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

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

    private void Start()
    {
        _gm = GameManager.Instance;
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

        StartCoroutine(CountTimer());
    }

    public void WatchAd()
    {
        _stopTimer = true;
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

        MetresText.text = "You run " + (int)_gm.DistanceRun + "m";

        NewRecordText.SetActive(ProgressManager.IsNewRecord(_gm.DistanceRun));

        RecordText.text = "Best: " + ProgressManager.DistanceRecord + "m";
        CoinsText.text = "Conis: " + _gm.CoinsThisRun;
    }

    public void ReviveForMoney()
    {
        if (ProgressManager.SpendCoins(_gm.ReviveCost)) 
        {
            Revive();
        }
        else //Если нам не хватило денег
        {
            //TODO Ожидание доната
        }
    }

}
