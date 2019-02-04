using UnityEngine;

public class PlayerPrefsCleaner : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
	}
}