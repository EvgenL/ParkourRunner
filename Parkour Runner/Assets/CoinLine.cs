using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;


public class CoinLine : CoinPoints
{

    public Mesh CoinMesh;
    public float MeshScale = 0.4f;


    public float CoinHeight = 1f;
    public float DistanceBetweenCoins = 2f;

     
    public override void Generate(GameObject lastBonus = null)
    {
        GameManager GameManager = GameManager.Instance;
        float scaleZ = transform.localScale.z;

        for (float i = 0; i < scaleZ; i += DistanceBetweenCoins)
        {
            //TODO POOL COINS?
            var coinGo = Instantiate(CoinPrefab, transform.position + new Vector3(0, CoinHeight, i) + transform.forward * i,
                Quaternion.AngleAxis(i * 10, Vector3.up));
            var coinScript = coinGo.GetComponent<Coin>();
            GameManager.Coins.Add(coinScript);

            //Last
            if (i + DistanceBetweenCoins >= scaleZ)
            {
                if (lastBonus != null)
                {
                    Instantiate(lastBonus, transform.position + new Vector3(0, CoinHeight, i) + transform.forward * i,
                        Quaternion.AngleAxis(i * 10, Vector3.up));
                }
                else
                {
                    Instantiate(CoinPrefab, transform.position + new Vector3(0, CoinHeight, i) + transform.forward * i,
                        Quaternion.AngleAxis(i * 10, Vector3.up));
                }
            }
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        float scaleZ = transform.localScale.z;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + new Vector3(0, CoinHeight, 0), 
            transform.position + transform.forward * scaleZ + new Vector3(0, CoinHeight, 0));
        var mesh = CoinPrefab.GetComponent<Mesh>();
        for (float i = 0; i < scaleZ; i += DistanceBetweenCoins)
        {
            if (CoinMesh != null)
            Gizmos.DrawWireMesh(CoinMesh, transform.position + new Vector3(0, CoinHeight, 0) + transform.forward * i,
                Quaternion.AngleAxis(i * 10, Vector3.up), Vector3.one * MeshScale);
        }
    }



}
