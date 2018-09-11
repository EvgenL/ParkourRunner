﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ParkourRunner.Scripts.Managers
{
    public class ResourcesManager : MonoBehaviour {


        private const string BlockPrefabsPath = "Blocks";
        private const string ObstaclePrefabsPath = "Obstacles";

        public List<GameObject> Obstacles3mPrefabs;


        public List<GameObject> ObstaclesSmallPrefabs;
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
            ObstaclesSmallPrefabs = Resources.LoadAll<GameObject>(ObstaclePrefabsPath + "/Small/").ToList();
            BlockPrefabs = Resources.LoadAll<GameObject>(BlockPrefabsPath + "/").ToList();
        }

    }
}