using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesManager : MonoBehaviour {


    private const string BlockPrefabsPath = "Blocks";
    private const string ObstaclePrefabsPath = "Obstacles";

    public List<GameObject> Obstacles3mPrefabs;
    public List<GameObject> BlockPrefabs;

    #region Singleton

    public static ResourcesManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

        LoadResources();
    }
    #endregion

    private void LoadResources()
    {
        Obstacles3mPrefabs = Resources.LoadAll<GameObject>(ObstaclePrefabsPath + "/3m/").ToList();
        BlockPrefabs = Resources.LoadAll<GameObject>(BlockPrefabsPath + "/").ToList();
    }

}
