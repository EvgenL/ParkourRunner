using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    #region Singleton
    public static SceneLoadManager Instance;

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

    public void LoadScene(string sceneName)
    { 
        SceneManager.LoadScene(sceneName);
    }
}
