using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Obstacle : GenerationPoint
{
    public List<GameObject> Prefabs = new List<GameObject>();
    //public CoinPoints JumpCoins; 

    public GameObject Object;

    public Mesh Mesh;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireMesh(Mesh, transform.position + transform.up * transform.localScale.y/2, transform.rotation, transform.localScale);
    }

    private void OnDrawGizmosSelected()
    {
        if (Mesh != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawMesh(Mesh, transform.position + transform.up * transform.localScale.y / 2, transform.rotation, transform.localScale);
        }
    }


    public override void Generate()
    {
        if (Prefabs.Count != 0)
        {
            var randomObs = Prefabs[Random.Range(0, Prefabs.Count)];
            Object = Instantiate(randomObs, transform.position, transform.rotation);
        }
    }
}
