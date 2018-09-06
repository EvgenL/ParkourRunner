using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Obstacle : GenerationPoint
{
    public float CoinChance = 0.3f;
    
    public Mesh Mesh;

    private void OnDrawGizmos()
    {
        if (DrawGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireMesh(Mesh, transform.position + transform.up * transform.lossyScale.y / 2,
                transform.rotation,
                transform.lossyScale);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (DrawGizmos)
        {
            if (Mesh != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawMesh(Mesh, transform.position + transform.up * transform.lossyScale.y / 2,
                    transform.rotation,
                    transform.lossyScale);
            }
        }
    }

    public override void Generate()
    {
        if (Used) return;

        var resourcesManager = ResourcesManager.Instance;
        GameObject randomObs = null;
        if (transform.lossyScale.z <= 3f)
        {
            int r = Random.Range(0, resourcesManager.Obstacles3mPrefabs.Count);
            randomObs = resourcesManager.Obstacles3mPrefabs
                [r];
        }
        else if (transform.lossyScale.z <= 6f)
        {

        }
        else if (transform.lossyScale.z <= 10f)
        {

        }
        else
        {
            Debug.LogError("Не найден подходящий префаб ", transform);
        }
        
        PoolManager.Instance.Spawn(randomObs, transform.position, transform.rotation);

        Used = true;

        //PoolManager.Instance.Remove(gameObject);
    }
}
