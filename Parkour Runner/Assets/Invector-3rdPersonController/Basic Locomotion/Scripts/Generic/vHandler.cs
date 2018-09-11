using System.Collections.Generic;
using UnityEngine;

namespace Basic_Locomotion.Scripts.Generic
{
    [System.Serializable]
    public class vHandler
    {
        public Transform defaultHandler;
        public List<Transform> customHandlers;
        public vHandler()
        {
            customHandlers = new List<Transform>();
        }
    }
}