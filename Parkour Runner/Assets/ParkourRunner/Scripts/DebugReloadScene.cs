using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugReloadScene : MonoBehaviour {

    public void Load(string sceneName)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
