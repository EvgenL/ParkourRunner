using ParkourRunner.Scripts.Player;
using RootMotion.Dynamics;
using UnityEngine;
using AEngine;

namespace ParkourRunner.Scripts.Track.Pick_Ups
{
    public class Landmine : MonoBehaviour
    {
        public float VetricalForce;
        public float UnpinForce;

        public ParticleSystem Explosion;

        private void OnTriggerEnter(Collider other)
        {
            Explosion.Play();
            AudioManager.Instance.PlaySound(Sounds.Explosion);

            var broadcaster = other.transform.GetComponent<MuscleCollisionBroadcaster>();
            if (broadcaster != null)
            {
                //Puppet.SetState(BehaviourPuppet.State.Unpinned);
                //Находим тазовую кость и даём в неё addforce
                broadcaster.puppetMaster.muscles[0].transform.GetComponent<Rigidbody>().AddForce(0, VetricalForce, 0, ForceMode.Impulse);

                //Это нога (возможно голова) которой игрок попал на мину
                broadcaster.Hit(UnpinForce, Vector3.up * UnpinForce, transform.position);
                var dism = broadcaster.GetComponent<MuscleDismember>();
                if (dism != null)
                {
                    dism.DismemberMuscleRecursive();
                }
            }
            gameObject.SetActive(false);

        }

    }
}
