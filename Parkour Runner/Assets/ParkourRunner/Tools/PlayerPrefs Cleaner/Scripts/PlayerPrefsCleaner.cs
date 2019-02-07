using UnityEngine;

public class PlayerPrefsCleaner : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        Invoke("ControlCleaner", 1f);
	}

    private void ControlCleaner()
    {
        Wallet.Instance.CleanAll();

        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}