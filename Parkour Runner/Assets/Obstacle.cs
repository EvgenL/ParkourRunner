using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public bool Used;

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




}
