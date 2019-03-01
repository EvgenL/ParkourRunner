using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Track.Generator;
using UnityEngine;

namespace ParkourRunner.Scripts.Track
{
    public class StandTrickGenerationPoint : GenerationPoint
    {
        public GameObject StandPrefab;

        private void OnDrawGizmos()
        {
            if (DrawGizmos)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, 1f);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (DrawGizmos)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(transform.position, 1f);
            }
        }


        public override void Generate()
        {
            Go = PoolManager.Instance.Spawn(StandPrefab, transform.position, transform.rotation);
            Used = true;
        }
    }
}
