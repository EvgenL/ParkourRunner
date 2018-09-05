using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class GenerationPoint : MonoBehaviour
    {
        public bool Used;
        public abstract void Generate();

        private void OnDestroy()
        {
            PoolManager.Instance.Remove(gameObject);
        }


    }
}
