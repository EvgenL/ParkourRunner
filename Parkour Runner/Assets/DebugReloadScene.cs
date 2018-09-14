using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugReloadScene : MonoBehaviour {

    public void Load(string sceneName)
    {
        SceneLoadManager.Instance.LoadScene(sceneName);
    }
}
