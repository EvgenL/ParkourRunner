using UnityEngine;
using UnityEngine.SceneManagement;
using AEngine;

public class ReloadScene : MonoBehaviour
{
    public void Reload()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(index);
        AudioManager.Instance.PlaySound(Sounds.Tap);

        while (unloadOperation != null && !unloadOperation.isDone)
        {
        }

        ParkourSlowMo.Instance.UnSlow();
        SceneManager.LoadScene(index);        
    }
}
