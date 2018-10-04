using System.Collections.Generic;
using System.Linq;
using ParkourRunner.Scripts.Player;
using UnityEngine;

namespace ParkourRunner.Scripts.Managers
{
    public class ResourcesManager : MonoBehaviour {


        private const string TricksPath = "Tricks";
        private const string BlockPrefabsPath = "Blocks";
        private const string ObstaclePrefabsPath = "Obstacles";
        private const string PickUpPath = "PickUp";

        public List<GameObject> BlockPrefabs;
        public List<GameObject> ObstaclesSmallPrefabs;
        
        public List<Trick> RollTricks;
        public List<Trick> JumpOverTricks;
        public List<Trick> StandTricks;

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
            //LoadResources();
        }
        #endregion

        /*//TODO asynch
        public void LoadResources()
        {
            BlockPrefabs = Resources.LoadAll<GameObject>(BlockPrefabsPath + "/").ToList();
            ObstaclesSmallPrefabs = Resources.LoadAll<GameObject>(ObstaclePrefabsPath + "/Small/").ToList();
            RollTricks = Resources.LoadAll<Trick>(TricksPath + "/Roll/").ToList();
            StandTricks = Resources.LoadAll<Trick>(TricksPath + "/Stand/").ToList();
            JumpOverTricks = Resources.LoadAll<Trick>(TricksPath + "/JumpOver/").ToList();
            PickUps = Resources.LoadAll<GameObject>(PickUpPath + "/").ToList();
        }*/

    }
}
