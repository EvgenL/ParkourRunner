using System.Collections.Generic;
using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Track.Pick_Ups;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Generator
{
    public class CoinSet : CoinPoints
    {

        [SerializeField] private List<Transform> _spots;

        public float ChanceToGenerateCoins = 0.3f;

        private void Start()
        {
            if (Random.Range(0f, 1f) < ChanceToGenerateCoins)
            {
                Generate();
            }
        }

        public override void Generate()
        {
            GameManager GameManager = GameManager.Instance;

            for (int i = 0; i < _spots.Count; i++)
            {
                var spot = _spots[i];

                var coinGo = PoolManager.Instance.Spawn(
                    CoinPrefab,
                    spot.position,
                    Quaternion.AngleAxis(i * 10, Vector3.up)
                );

                var coinScript = coinGo.GetComponent<Coin>();
                GameManager.Coins.Add(coinScript);
            }

            Used = true;
        }
    }
}
