using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Track.Pick_Ups;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Generator
{
    public class CoinLine : CoinPoints
    {

        public Mesh CoinMesh;
        public float MeshScale = 0.4f;


        public float CoinHeight = 1f;
        public float DistanceBetweenCoins = 2f;

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


        public override void Generate()
        {
            if (Used) return;

            float scaleZ = transform.localScale.z;

            for (float i = 0; i < scaleZ; i += DistanceBetweenCoins)
            {
                var coinGo = PoolManager.Instance.Spawn(
                    CoinPrefab,
                    transform.position + new Vector3(0, CoinHeight, 0) + transform.forward * i,
                    Quaternion.AngleAxis(i * 10, Vector3.up)
                );

                var coinScript = coinGo.GetComponent<Coin>();
                PoolManager.Instance.Coins.Add(coinScript);
            }

            Used = true;
        }
    }
}
