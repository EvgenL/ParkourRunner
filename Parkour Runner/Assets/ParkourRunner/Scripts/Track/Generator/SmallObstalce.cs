using ParkourRunner.Scripts.Managers;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Generator
{
    class SmallObstalce : GenerationPoint
    {
        private void OnDrawGizmos()
        {
            if (DrawGizmos)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, 0.5f);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (DrawGizmos)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(transform.position, 0.5f);
            }
        }
        public override void Generate()
        {
            if (Used) return;

            var resourcesManager = ResourcesManager.Instance;
            GameObject randomObs = null;

            int r = UnityEngine.Random.Range(0, resourcesManager.ObstaclesSmallPrefabs.Count);
            randomObs = resourcesManager.ObstaclesSmallPrefabs
                [r];

            Go = PoolManager.Instance.Spawn(randomObs, transform.position, transform.rotation);

            Used = true;
        }
    }
}
