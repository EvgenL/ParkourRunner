using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Block : MonoBehaviour
{
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


}
