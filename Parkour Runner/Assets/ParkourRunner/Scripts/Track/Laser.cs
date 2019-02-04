using ParkourRunner.Scripts.Player;
using ParkourRunner.Scripts.Managers;
using RootMotion.Dynamics;
using UnityEngine;
using AEngine;

namespace ParkourRunner.Scripts.Track
{
    public class Laser : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var broadcaster = other.attachedRigidbody.GetComponent<MuscleCollisionBroadcaster>();

            // If is a muscle...
            if (broadcaster != null)
            {
                if (GameManager.Instance.gameState != GameManager.GameState.Dead)
                {
                    AudioManager.Instance.PlayUniqueSound(Sounds.Laser);
                }

                //broadcaster.Hit(unpin, ray.direction * force, hit.point);

                // Remove the muscle and its children
                //broadcaster.puppetMaster.RemoveMuscleRecursive(broadcaster.puppetMaster.muscles[broadcaster.muscleIndex].joint, true, true, removeMuscleMode);

                var dismember = broadcaster.transform.GetComponent<MuscleDismember>();
                if (dismember != null)
                {
                    dismember.DismemberMuscleRecursive();
                }
            }
            else
            {
                // Not a muscle (any more)
                var joint = other.attachedRigidbody.GetComponent<ConfigurableJoint>();
                if (joint != null) Destroy(joint);
            }
            //TODO PlayParticle
            /*
        // Particle FX
        particles.transform.position = hit.point;
        particles.transform.rotation = Quaternion.LookRotation(-ray.direction);
        particles.Emit(5);*/
        }

    }
}
