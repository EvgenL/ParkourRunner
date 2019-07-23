using UnityEngine;
//using GoogleMobileAds.Api;

public class AdMobController : MonoBehaviour //BaseAdController
{
    /*
    private const string AD_MOB_GAME_IOS_ID = "ca-app-pub-9017460316126624~2209772973";
    private const string INTERSTITIAL_IOS_ID = "ca-app-pub-9017460316126624/9208573399";

    private InterstitialAd _ad;

    public override void Initialize()
    {

#if UNITY_IOS
        MobileAds.Initialize(AD_MOB_GAME_IOS_ID);
        _ad = new InterstitialAd(INTERSTITIAL_IOS_ID);
#elif UNITY_ANDROID
        // Рекламный блок под Android еще не создан
#elif UNITY_EDITOR
        MobileAds.Initialize(AD_MOB_GAME_IOS_ID);
        _ad = new InterstitialAd(INTERSTITIAL_IOS_ID);
#endif
        
        AdRequest request = new AdRequest.Builder().Build();
        _ad.LoadAd(request);

        // Без награды. Это не Rewarded, по идее видео не должен просто так закрыть пока не закончится, так что любой показ - как награда.
        _ad.OnAdClosed -= OnAdClosed;
        _ad.OnAdFailedToLoad -= OnAdFailed;
        _ad.OnAdClosed += OnAdClosed;
        _ad.OnAdFailedToLoad += OnAdFailed;
    }

    public override bool IsAvailable()
    {
        return _ad.IsLoaded();
    }

    public override void ShowAdvertising()
    {
        _ad.Show();
    }

    #region Events
    private void OnAdClosed(object sender, System.EventArgs args)
    {
        HandleAdResult(UnityEngine.Advertisements.ShowResult.Finished);
    }

    private void OnAdFailed(object sender, System.EventArgs args)
    {
        HandleAdResult(UnityEngine.Advertisements.ShowResult.Finished);
    }
    #endregion
    */
}