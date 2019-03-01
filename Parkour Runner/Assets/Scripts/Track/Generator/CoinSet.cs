using System.Collections.Generic;
using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Track.Pick_Ups;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Generator
{
    public class CoinSet : CoinPoints
    {

        [SerializeField] private List<Transform> _spots;


        public override void Generate()
        {
            for (int i = 0; i < _spots.Count; i++)
            {
                var spot = _spots[i];

                if (CoinPrefab == null)
                    CoinPrefab = Resources.Load<GameObject>("PickUp/Coin"); //TODO TEST

                var coinGo = PoolManager.Instance.Spawn(
                    CoinPrefab,
                    spot.position,
                    Quaternion.AngleAxis(i * 10, Vector3.up)
                );

                var coinScript = coinGo.GetComponent<Coin>();
                PoolManager.Instance.Coins.Add(coinScript);
            }

            Used = true;
        }
    }
}
