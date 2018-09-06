using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class GenerationPoint : MonoBehaviour
    {
        public bool DrawGizmos = true;
        public bool Used;
        public abstract void Generate();

        private void OnDestroy()
        {
            if (PoolManager.Instance != null)
            PoolManager.Instance.Remove(gameObject);
        }


    }
}
