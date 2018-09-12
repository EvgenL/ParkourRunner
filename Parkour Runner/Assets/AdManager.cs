using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour {

    #region Singleton

    public static AdManager Instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion


    public delegate void Callback();

    private Callback _callback;

    public void ShowVideo(Callback callback)
    {
        this._callback = callback;
        if (Advertisement.IsReady())
        {
            Advertisement.Show("", new ShowOptions(){resultCallback = HandleAdResult});
        }
    }

    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                print("Ad Finished");
                _callback();
                break;
            case ShowResult.Skipped:
                print("Ad Skipped");
                break;
            case ShowResult.Failed:
                print("Ad Failed");
                break;
        }
    }
}
