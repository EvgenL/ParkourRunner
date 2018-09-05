using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Pick_Ups;
using UnityEngine;

public class StandTrick : GenerationPoint
{
    public GameObject StandPrefab;

    public bool DrawGizmo = true;

    private void OnDrawGizmos()
    {
        if (DrawGizmo)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (DrawGizmo)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 1f);
        }
    }


    public override void Generate()
    {
        PoolManager.Instance.Spawn(StandPrefab, transform.position, transform.rotation);
    }
}
