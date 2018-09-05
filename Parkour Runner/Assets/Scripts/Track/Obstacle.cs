using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Obstacle : GenerationPoint
{
    public float CoinChance = 0.3f;
    public GameObject[] Prefabs;
    
    public Mesh Mesh;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireMesh(Mesh, transform.position + transform.up * transform.lossyScale.y / 2, transform.rotation,
            transform.lossyScale);
    }

    private void OnDrawGizmosSelected()
    {
        if (Mesh != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawMesh(Mesh, transform.position + transform.up * transform.lossyScale.y / 2, transform.rotation,
                transform.lossyScale);
        }
    }

    public override void Generate()
    {
        if (Prefabs.Length != 0)
        {
            var randomObs = Prefabs[Random.Range(0, Prefabs.Length)];
            PoolManager.Instance.Spawn(randomObs, transform.position, transform.rotation);
        }
    }
}
