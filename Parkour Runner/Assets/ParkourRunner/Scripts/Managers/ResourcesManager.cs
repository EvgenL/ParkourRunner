﻿using System.Collections.Generic;
using System.Linq;
using ParkourRunner.Scripts.Player;
using UnityEngine;
using Assets.ParkourRunner.Scripts.Track.Generator;
namespace ParkourRunner.Scripts.Managers
{
    public class ResourcesManager : MonoBehaviour
    {


       [SerializeField] private Res _res;
        private const string TricksPath = "Tricks";
        private const string BlockPrefabsPath = "Blocks";
        private const string ObstaclePrefabsPath = "Obstacles";
        private const string PickUpPath = "PickUp";

        public static List<GameObject> BlockPrefabs;
        public static List<GameObject> ObstaclesSmallPrefabs;
        
        public static List<Trick> RollTricks;
        public static List<Trick> JumpOverTricks;
        public static List<Trick> StandTricks;
        public static List<Trick> SlideTricks;

        public List<GameObject> PickUps;

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

            DontDestroyOnLoad(gameObject);
            LoadResources();
        }
        #endregion

        public void LoadResources()
        {
            BlockPrefabs = _res.BlockPrefabs.FindAll(x => x.activeSelf);
            
            ObstaclesSmallPrefabs = _res.ObstaclesSmallPrefabs;
            RollTricks = _res.RollTricks;
            JumpOverTricks = _res.JumpOverTricks;
            StandTricks = _res.StandTricks;
            SlideTricks = _res.SlideTricks;
        }
        
    }
}
