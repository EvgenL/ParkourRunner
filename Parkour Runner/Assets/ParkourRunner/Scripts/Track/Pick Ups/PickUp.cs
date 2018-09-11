using ParkourRunner.Scripts.Managers;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Pick_Ups
{
    public abstract class PickUp : MonoBehaviour
    {
        protected abstract void Pick();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Pick();
                PoolManager.Instance.Remove(gameObject);
            }
        }

    }
}
