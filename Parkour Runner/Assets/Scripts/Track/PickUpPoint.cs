using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class PickUpPoint : GenerationPoint
{
    public GameObject[] Prefabs;


    //public GameObject MagnetPrefab;
    //public GameObject JumpPrefab;
    //public GameObject ShieldPrefab;
    //public GameObject HealPrefab;
    //public GameObject X2Prefab;
    //public GameObject BoostPrefab;


    public Vector3 BonusPosition = new Vector3(0, 1, 0);
    public bool DrawGizmo = true;

    private void OnDrawGizmos()
    {
        if (DrawGizmo)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position + BonusPosition, 0.5f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (DrawGizmo)
        {
            Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(transform.position + BonusPosition, 0.5f);
        }
    }

    public override void Generate()
    {
        var randPrefab = Prefabs[Random.Range(0, Prefabs.Length)];
        PoolManager.Instance.Spawn(randPrefab, transform.position + BonusPosition, Quaternion.identity);
    }
}
