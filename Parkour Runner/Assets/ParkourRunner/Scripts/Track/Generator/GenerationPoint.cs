using ParkourRunner.Scripts.Managers;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Generator
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
