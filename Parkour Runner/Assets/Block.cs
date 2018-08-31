using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Track;
using UnityEngine;


[ExecuteInEditMode]
public class Block : MonoBehaviour
{
    void Update()
    {
        if (!Application.isPlaying)
        {
            if (GPoints.Count == 0)
            {
                GPoints = GetComponentsInChildren<GenerationPoint>().ToList();
            }
        }
    }

    public enum BlockType
    {
        Houses,
        Flat,
        Start
    }

    public BlockType Type;
    public Block Next;
    public Block Prev;
    public Block Left;
    public Block Right;

    public List<GenerationPoint> GPoints = new List<GenerationPoint>();

    public void GenerateCallibration(float positionZ, float stateLength)
    {
        foreach (var point in GPoints)
        {
            if (point.transform.position.z > positionZ &&
                point.transform.position.z < positionZ + stateLength)
            {
                point.Generate();
                point.Used = true;
                Debug.DrawRay(point.transform.position, Vector3.up * 5f, Color.red, 5f);
            }
        }
    }
}
