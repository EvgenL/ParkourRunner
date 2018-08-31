using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Track;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class CoinPoints : GenerationPoint
    {
        public bool IsInObstacle = false;
        public GameObject CoinPrefab;
    }
}
