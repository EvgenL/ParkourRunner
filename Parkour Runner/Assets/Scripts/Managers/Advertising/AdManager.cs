using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    #region Singleton
    public static AdManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            ResetAdvertisingOrder();

            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion
        
    [SerializeField] private BaseAdController[] _advertisings;

    private int _gameSessionCount;

    public bool EnableAds { get { return PlayerPrefs.GetInt("NoAds") != 1; } }

    public void Start()
    {
        foreach (BaseAdController ad in _advertisings)
            ad.Initialize();
    }

    public bool IsAvailable()
    {
        foreach (BaseAdController ad in _advertisings)
            if (ad.IsAvailable())
                return true;

        return false;
    }

    public void ShowAdvertising(Action finishedCallback, Action skippedCallback, Action failedCallback)
    {
        bool isShowingAd = false;

        foreach (BaseAdController ad in _advertisings)
        {
            if (ad.IsAvailable())
            {
                Debug.Log("Show " + ad.gameObject.name);
                ad.InitCallbackHandlers(finishedCallback, skippedCallback, failedCallback);

                if (this.EnableAds)
                    ad.ShowAdvertising();
                else
                    ad.HandleAdResult(ShowResult.Finished);

                isShowingAd = true;
                break;
            }
        }

        if (!isShowingAd)
        {
            Debug.Log("Ad not show");
            finishedCallback.SafeInvoke();
        }
    }

    #region Advertising Order
    public bool CheckAdvertisingOrder()
    {
        if (PlayerPrefs.GetInt(EnvironmentController.TUTORIAL_KEY) == 1 || _gameSessionCount % 3 == 0 || !AdManager.Instance.IsAvailable())
        {
            ResetAdvertisingOrder();
            return false;
        }

        _gameSessionCount++;

        return true;
    }

    public void ResetAdvertisingOrder()
    {
        _gameSessionCount = 1;
    }

    public void SkipAdInOrder()
    {
        _gameSessionCount = 3;
    }
    #endregion
}