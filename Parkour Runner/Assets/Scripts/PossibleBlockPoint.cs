using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class PossibleBlockPoint
    {
        public Vector3 Position;
        public Block Next;
        public Block Prev;
        public Block Left;
        public Block Right;
    }
}   
