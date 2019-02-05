using System.Collections.Generic;
using ParkourRunner.Scripts.Track.Generator;
using ParkourRunner.Scripts.Player;
using UnityEngine;
using System;

namespace Assets.ParkourRunner.Scripts.Track.Generator
{
    [CreateAssetMenu(fileName = "res", menuName = "ParkouRunner/res")]
    public class Res : ScriptableObject
    {
        [Serializable]
        public class DefaulEnvironmentSettings
        {
            public int startCount;
            public int separateCount;
            public float nextWeight;
            public Block startPoint;
            public List<Block> blocks;
        }

        [Serializable]
        public class SpecialEnvironmentSettings
        {
            public int minCount;
            public int maxCount;
            public float nextWeight;
            public Block startPoint;
            public Block finishPoint;
            public List<Block> blocks;
        }

        [SerializeField] private DefaulEnvironmentSettings _defaultEnvironment;
        [SerializeField] private List<SpecialEnvironmentSettings> _specialEnvironments;

        public  List<GameObject> ObstaclesSmallPrefabs;
        public  List<Trick> RollTricks;
        public  List<Trick> JumpOverTricks;
        public List<Trick> StandTricks;
        public List<Trick> SlideTricks;

        public DefaulEnvironmentSettings DefaultEnvironment { get { return _defaultEnvironment; } }
        public List<SpecialEnvironmentSettings> SpecialEnvironments { get { return _specialEnvironments; } }
    }
}
