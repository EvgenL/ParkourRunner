using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdController : BaseAdController
{
    private const string IOS_AD_ID = "3215465";
    private const string ANDROID_AD_ID = "3215464";

    public override void Initialize()
    {
        if (Advertisement.isSupported)
        {
#if UNITY_IOS
            Advertisement.Initialize(IOS_AD_ID);
#elif UNITY_ANDROID
        Advertisement.Initialize(ANDROID_AD_ID);
#elif UNITY_EDITOR
        // Условно id под iOS, тестовый режим
        Advertisement.Initialize(IOS_AD_ID, true);
#endif
        }
        else
            Debug.Log("Advertising platform is not suported");
    }

    public override bool IsAvailable()
    {
        return Advertisement.isSupported && Advertisement.IsReady();
    }

    public override void ShowAdvertising()
    {
        Advertisement.Show(new ShowOptions() { resultCallback = HandleAdResult });
    }
}
