using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public bool Used;

    public Vector3 GizmoSize;

    public Mesh Mesh;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireMesh(Mesh, transform.position + transform.up *  GizmoSize.y/2, transform.rotation, GizmoSize);
    }

    private void OnDrawGizmosSelected()
    {
        if (Mesh != null)
        {
            Gizmos.DrawMesh(Mesh, transform.position + transform.up * GizmoSize.y / 2, transform.rotation, GizmoSize);
        }
    }




}
