using System.Collections;
using System.Collections.Generic;
using ParkourRunner.Scripts.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour {

    public void Reload()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(index);

        while (unloadOperation != null && !unloadOperation.isDone)
        {
        }

        SceneManager.LoadScene(index);
    }
}
