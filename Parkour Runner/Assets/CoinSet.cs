using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class CoinSet : CoinPoints
{

   [SerializeField] private List<Transform> _spots;

    

    public override void Generate(GameObject lastBonus = null)
    {
        GameManager GameManager = GameManager.Instance;
        
        for (int i = 0; i < _spots.Count; i++)
        {
            var spot = _spots[i];

            var coinGo = Instantiate(CoinPrefab, spot.position,
                Quaternion.AngleAxis(i * 10, Vector3.up));
            var coinScript = coinGo.GetComponent<Coin>();
            GameManager.Coins.Add(coinScript);

            if (i + 1 == _spots.Count)
            {
                if (lastBonus != null)
                {
                    Instantiate(lastBonus, spot.position,
                        Quaternion.AngleAxis(i * 10, Vector3.up));
                }
                else
                {
                    Instantiate(CoinPrefab, spot.position,
                        Quaternion.AngleAxis(i * 10, Vector3.up));
                }
            }
        }
        Destroy(gameObject);
    }
}
