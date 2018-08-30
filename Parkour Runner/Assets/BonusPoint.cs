using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPoint : MonoBehaviour
{

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


}
