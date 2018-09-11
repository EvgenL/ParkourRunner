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

        public List<GameObject> Obstacles3mPrefabs;
        //TODO public List<GameObject> Obstacles5mPrefabs;


        public List<GameObject> ObstaclesSmallPrefabs;
        public List<GameObject> BlockPrefabs;

        public List<Trick> RollTricks;
        public List<Trick> JumpOverTricks;
        public List<Trick> StandTricks;

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

        //TODO asynch
        private void LoadResources()
        {
            Obstacles3mPrefabs = Resources.LoadAll<GameObject>(ObstaclePrefabsPath + "/3m/").ToList();
            ObstaclesSmallPrefabs = Resources.LoadAll<GameObject>(ObstaclePrefabsPath + "/Small/").ToList();
            BlockPrefabs = Resources.LoadAll<GameObject>(BlockPrefabsPath + "/").ToList();
            //RollTricks = Resources.LoadAll<Trick>(TricksPath + "/Roll/").ToList();
            //StandTricks = Resources.LoadAll<Trick>(TricksPath + "/Stand/").ToList();
            JumpOverTricks = Resources.LoadAll<Trick>(TricksPath + "/JumpOver/").ToList();
        }

    }
}
