using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class CoinSet : CoinPoints
{

   [SerializeField] private List<Transform> _spots;

    public override void Generate()
    {
        GameManager GameManager = GameManager.Instance;

        for (int i = 0; i < _spots.Count; i++)
        {
            var spot = _spots[i];

            var coinGo = Instantiate(CoinPrefab, spot.position,
                Quaternion.AngleAxis(i * 10, Vector3.up));
            var coinScript = coinGo.GetComponent<Coin>();
            GameManager.Coins.Add(coinScript);
        }
    }
}
