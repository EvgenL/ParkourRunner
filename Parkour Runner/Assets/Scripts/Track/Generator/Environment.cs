using System.Collections.Generic;
using ParkourRunner.Scripts.Track.Generator;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Environment", menuName = "ParkouRunner/Environment")]
public class Environment : ScriptableObject
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

    [Serializable]
    public class LevelEnvironmentSettings
    {
        public int blocksCount;
        public Block start;
        public Block finish;
        public List<Block> blocks;
    }

    [Serializable]
    public class TutorialEnvironmentSettings
    {
        public List<Block> blocks;
    }

    [SerializeField] private int _levelIndex;
    [SerializeField] private DefaulEnvironmentSettings _defaultEnvironment;
    [SerializeField] private List<SpecialEnvironmentSettings> _specialEnvironments;
    [SerializeField] private LevelEnvironmentSettings _levelEnvironment;
    [SerializeField] private TutorialEnvironmentSettings _tutorialEnvironment;
    [SerializeField] private bool _endlessLevel;

    public int LevelIndex { get { return _levelIndex; } }
    public DefaulEnvironmentSettings DefaultEnvironment { get { return _defaultEnvironment; } }
    public List<SpecialEnvironmentSettings> SpecialEnvironments { get { return _specialEnvironments; } }
    public LevelEnvironmentSettings LevelEnvironment { get { return _levelEnvironment; } }
    public TutorialEnvironmentSettings TutorialEnvironment { get { return _tutorialEnvironment; } }
    public bool EndlessLevel { get { return _endlessLevel; } }
    public bool TutorialLevel { get { EnvironmentController.CheckKeys(); return PlayerPrefs.GetInt(EnvironmentController.TUTORIAL_KEY) == 1; } }
}