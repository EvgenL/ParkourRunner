using UnityEngine;
using UnityEngine.UI;
using AEngine;

public class PauseAd : MonoBehaviour
{
    [SerializeField] private Image _buttonImage;
    [SerializeField] private int _reward;

    private void OnEnable()
    {
        _buttonImage.enabled = AdManager.Instance.IsAvailable();
    }

    public void OnButtonClick()
    {
        if (AdManager.Instance.IsAvailable())
        {
            AudioManager.Instance.PlaySound(Sounds.Tap);
            AdManager.Instance.ShowAdvertising(AdFinishedCallback, AdSkippedCallback, AdSkippedCallback);
        }
    }

    private void AdFinishedCallback()
    {
        Wallet.Instance.AddCoins(_reward, Wallet.WalletMode.InGame);
    }

    private void AdSkippedCallback()
    {
        Debug.Log("Ad was not finished");
    }
}