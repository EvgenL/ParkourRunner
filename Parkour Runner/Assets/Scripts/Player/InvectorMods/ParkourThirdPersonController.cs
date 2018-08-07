using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Invector.CharacterController;
using RootMotion.Dynamics;
using UnityEngine;

namespace Assets.Scripts.Player.InvectorMods
{
    class ParkourThirdPersonController : vThirdPersonController
    {
        public LayerMask InRollCollisions;
        public BehaviourPuppet Puppet;
        
        public float RollKnockOutDistance = 4f;
        public float _oldKnockOutDistance;

        private LayerMask _oldCollisions;

        void Awake()
        {
            base.Awake();
        }

        public void Update()
        {
            ControllRollRagdollCollisions();
        }

        /*TODO
        bool ragdolled
        {
            get { return Puppet.}
        }*/

        //Позволяет врезаться головой в стену во всемя переката
        private void ControllRollRagdollCollisions()
        {
            if (isRolling && Puppet.collisionLayers != InRollCollisions)
            {
                _oldCollisions = Puppet.collisionLayers;
                Puppet.collisionLayers = InRollCollisions;

                _oldKnockOutDistance = Puppet.knockOutDistance;
                Puppet.knockOutDistance = RollKnockOutDistance;
            }
            else if (!isRolling && Puppet.collisionLayers == InRollCollisions)
            {
                Puppet.collisionLayers = _oldCollisions;
                Puppet.knockOutDistance = _oldKnockOutDistance;
            }
        }

        public override void Roll()
        {
            bool staminaCondition = currentStamina > rollStamina;
            // can roll even if it's on a quickturn or quickstop animation
            bool actionsRoll = !actions || (actions && (quickStop));
            // general conditions to roll
            bool rollConditions = (input != Vector2.zero || speed > 0.25f) && actionsRoll && isGrounded && staminaCondition && !isJumping;

            if (!rollConditions || isRolling) return;

            string randomRoll = Tricks.GetRandomRoll();
            animator.CrossFadeInFixedTime(randomRoll, 0.1f);


        }
    }
}
