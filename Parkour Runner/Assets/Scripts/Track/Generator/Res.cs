using System.Collections.Generic;
using ParkourRunner.Scripts.Player;
using UnityEngine;

namespace Assets.ParkourRunner.Scripts.Track.Generator
{
    [CreateAssetMenu(fileName = "res", menuName = "ParkouRunner/res")]
    public class Res : ScriptableObject
    {
        public  List<GameObject> ObstaclesSmallPrefabs;
        public  List<Trick> RollTricks;
        public  List<Trick> JumpOverTricks;
        public List<Trick> StandTricks;
        public List<Trick> SlideTricks;
    }
}
