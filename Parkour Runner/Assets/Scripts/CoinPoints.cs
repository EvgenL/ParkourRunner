using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class CoinPoints : MonoBehaviour
    {
        void Start()
        {
            //TODO DEBUG
            Generate();
        }

        public GameObject CoinPrefab;
        public abstract void Generate(GameObject lastBonus = null);
    }
}
