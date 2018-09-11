﻿using RootMotion.Dynamics;
using UnityEngine;

namespace ParkourRunner.Scripts.Player
{
    public class DismemberShooter : MonoBehaviour {

        public LayerMask layers;
        public float unpin = 10f;
        public float force = 10f;
        public ParticleSystem particles;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // Raycast to find a ragdoll collider
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit, 100f, layers))
                {
                    var broadcaster = hit.collider.attachedRigidbody.GetComponent<MuscleCollisionBroadcaster>();

                    // If is a muscle...
                    if (broadcaster != null)
                    {
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
                        var joint = hit.collider.attachedRigidbody.GetComponent<ConfigurableJoint>();
                        if (joint != null) Destroy(joint);

                        // Add force
                        hit.collider.attachedRigidbody.AddForceAtPosition(ray.direction * force, hit.point);
                    }
                    /*
                // Particle FX
                particles.transform.position = hit.point;
                particles.transform.rotation = Quaternion.LookRotation(-ray.direction);
                particles.Emit(5);*/
                }
            }
        }
    }
}
