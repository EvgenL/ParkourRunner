using ParkourRunner.Scripts.Managers;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Generator
{
    public abstract class GenerationPoint : MonoBehaviour
    {
        public bool DrawGizmos = true;
        public bool Used;
        public abstract void Generate();
        public GameObject Go;

        private void OnDestroy()
        {
            if (PoolManager.Instance != null)
            PoolManager.Instance.Remove(gameObject);
        }

        public void Clear()
        {
            if (Used)
            {
                Used = false;

                if (Go != null)
                    Destroy(Go);
            }
        }

        public void Delete()
        {
            if (Go != null)
                Destroy(Go);
        }
    }
}
