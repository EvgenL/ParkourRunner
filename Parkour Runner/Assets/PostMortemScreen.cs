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

    public GameObject ResultsScreen;
    public Text CoinsText;
    public Text MetresText;
    public Text RecordText;
    public GameObject NewRecordText;

    public float TimeToRevive = 5f;

    private GameManager _gm;
    private bool _alive = true;
    private bool _adSeen; //Игрок уже смотрел рекламу?

    private void Start()
    {
        _gm = GameManager.Instance;
    }

    public void Show()
    {
        _alive = false;
        ReviveScreen.SetActive(true);

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
        _adSeen = true;
        Revive();
        ReviveScreen.SetActive(false);
        ResultsScreen.SetActive(false);
        AdManager.Instance.ShowVideo(AdFinishedCallback);
    }

    private void AdFinishedCallback()
    {
        _gm.Revive();
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
        StopCoroutine(CountTimer());
        ReviveScreen.SetActive(false);
    }

    public void ExitReviveScreen()
    {
        ReviveScreen.SetActive(false);
        ResultsScreen.SetActive(true);

        MetresText.text = "You run " + _gm.DistanceRun + "m";

        NewRecordText.SetActive(ProgressManager.IsNewRecord(_gm.DistanceRun));

        RecordText.text = "Best: " + ProgressManager.DistanceRecord + "m";
        CoinsText.text = "Conis: " + _gm.CoinsThisRun;
    }

}
