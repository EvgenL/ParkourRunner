using System.Collections.Generic;
using UnityEngine;

namespace Basic_Locomotion.Scripts.ObjectDamage.vSpike
{
    public class vSpikeControl : MonoBehaviour
    {
        [HideInInspector]
        public List<Transform> attachColliders;
   
        void Start()
        {
            attachColliders = new List<Transform>();
            var objs = GetComponentsInChildren<vSpike>();
            foreach (vSpike obj in objs)
                obj.control = this;
        }
    }
}
