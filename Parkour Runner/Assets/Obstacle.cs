using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public bool Used;

    public Vector3 GizmoSize = Vector3.one;

    public Mesh Mesh;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (Mesh != null)
        {
            GizmoSize = Mesh.bounds.size;
        }
        else
        {
            Gizmos.DrawWireCube(transform.position + Vector3.up *  (GizmoSize.y/2), GizmoSize);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Mesh != null)
        {
            Gizmos.DrawMesh(Mesh, transform.position, transform.rotation);
        }
    }




}
