using System.Collections.Generic;
using System.Linq;
using ParkourRunner.Scripts.Managers;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Generator
{
    public class PickUpPoint : GenerationPoint
    {
        public List<GameObject> Prefabs;


        //public GameObject MagnetPrefab;
        //public GameObject JumpPrefab;
        //public GameObject ShieldPrefab;
        //public GameObject HealPrefab;
        //public GameObject X2Prefab;
        //public GameObject BoostPrefab;


        public Vector3 BonusPosition = new Vector3(0, 1, 0);

        private void OnDrawGizmos()
        {
            if (DrawGizmos)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawWireSphere(transform.position + BonusPosition, 0.5f);
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (DrawGizmos)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(transform.position + BonusPosition, 0.5f);
            }
        }

        public override void Generate()
        {
            Prefabs = ResourcesManager.Instance.PickUps;
            var randPrefab = Prefabs[Random.Range(0, Prefabs.Count)];
            PoolManager.Instance.Spawn(randPrefab, transform.position + BonusPosition, Quaternion.identity);
            Used = true;
        }
    }
}
