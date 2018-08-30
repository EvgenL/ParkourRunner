using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinLine : MonoBehaviour
{
    public GameObject CoinPrefab;
    public GameObject BigCoinPrefab;

    public Mesh CoinMesh;
    public float MeshScale = 0.4f;

    

    public float CoinHeight = 1f;
    public float DistanceBetweenCoins = 2f;

     void Start()
     {
         //TODO DEBUG
         SpawnCoins();
     }

    public void SpawnCoins(GameObject lastBonus = null)
    {
        float scaleZ = transform.localScale.z;

        for (float i = 0; i < scaleZ; i += DistanceBetweenCoins)
        {
            Instantiate(CoinPrefab, transform.position + new Vector3(0, CoinHeight, i) + transform.forward * i,
                Quaternion.AngleAxis(i * 10, Vector3.up));

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
