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
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion
        
    [SerializeField] private BaseAdController[] _advertisings;

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

    public void ShowVideo(Action finishedCallback, Action skippedCallback, Action failedCallback)
    {
        bool isShowingAd = false;

        foreach (BaseAdController ad in _advertisings)
        {
            if (ad.IsAvailable())
            {
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
}
