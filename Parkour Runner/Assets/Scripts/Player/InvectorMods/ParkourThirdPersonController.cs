using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Invector.CharacterController;
using Invector.CharacterController.Actions;
using RootMotion.Dynamics;
using UnityEngine;

namespace Assets.Scripts.Player.InvectorMods
{
    class ParkourThirdPersonController : vThirdPersonController
    {
        public LayerMask InRollCollisions;
        public BehaviourPuppet PuppetBehav;
        public PuppetMaster PuppetMaster;
        public Weight RollPuppetCollisionResistance;

        public bool IsSlidingDown = false;
        public bool IsSlidingTrolley = false;
        public bool IsRunningWall = false;
        public bool IsUsingHook = false;

        public float RollKnockOutDistance = 4f;
        public float _oldKnockOutDistance;

        public float HookSpeed = 2f;

        [HideInInspector] public Vector3 TrolleyOffset;
        [HideInInspector] public Vector3 WallOffset;
        [HideInInspector] public Vector3 HookOffset;

        /*[HideInInspector]*/ public Transform TargetTransform;

        //Чисто по приколу сделал чтоб он держался за IK пока едет на тарзанке
        [HideInInspector] public AvatarIKGoal TrolleyHand;
        void OnAnimatorIK(int layerIndex)
        {
            if (!IsSlidingTrolley) return;

            var target = GetComponent<vGenericAction>().triggerAction.avatarTarget;

            animator.SetIKPositionWeight(TrolleyHand, 0.7f);
            animator.SetIKPosition(TrolleyHand, TargetTransform.position);
        }



        private LayerMask _oldCollisions;
        private Weight _oldCollisionResistance;
        //private ParkourThirdPersonInput parkourInput;
        
        private new void Start()
        {
            base.Start();
            //parkourInput = GetComponent<ParkourThirdPersonInput>();
        }

        private void Update()
        {
            //ControllRollRagdoll();
            ControllStates();
        }

        private void FixedUpdate()
        {
            if (IsSlidingTrolley)
            {
                transform.position = TargetTransform.position + TrolleyOffset;
            }
            if (IsRunningWall)
            {
                transform.position = TargetTransform.position + WallOffset;
            }
            if (IsUsingHook)
            {
                transform.position = Vector3.MoveTowards(transform.position, TargetTransform.position + HookOffset, HookSpeed * Time.fixedDeltaTime);
                transform.rotation = TargetTransform.rotation;
                if (Vector3.Distance(TargetTransform.position + HookOffset, transform.position) <= (HookSpeed * Time.deltaTime))
                {
                    IsUsingHook = false;
                }
            }

            if (customAction)
            {
                if (PuppetMaster.mode != PuppetMaster.Mode.Disabled)
                    PuppetMaster.mode = PuppetMaster.Mode.Disabled;
            }
            else
            {
                if (PuppetMaster.mode != PuppetMaster.Mode.Active)
                    PuppetMaster.mode = PuppetMaster.Mode.Active;
            }
        }

        private void ControllStates()
        {
            animator.SetBool("IsRunningWall", IsRunningWall);
            animator.SetBool("IsSlidingDown", IsSlidingDown);
            animator.SetBool("IsSlidingTrolley", IsSlidingTrolley);
            animator.SetBool("IsUsingHook", IsUsingHook);
        }


        //Позволяет врезаться головой в default layer во время переката. Работает хуйово, так что я выключил.
        private void ControllRollRagdoll()
        {

            if (isRolling && PuppetBehav.collisionLayers != InRollCollisions)
            {
                _oldCollisions = PuppetBehav.collisionLayers;
                PuppetBehav.collisionLayers = InRollCollisions;

                _oldKnockOutDistance = PuppetBehav.knockOutDistance;
                PuppetBehav.knockOutDistance = RollKnockOutDistance;

                _oldCollisionResistance = PuppetBehav.collisionResistance;
                PuppetBehav.collisionResistance = RollPuppetCollisionResistance;
            }
            else if (!isRolling && PuppetBehav.collisionLayers == InRollCollisions)
            {
                PuppetBehav.collisionLayers = _oldCollisions;
                PuppetBehav.knockOutDistance = _oldKnockOutDistance;
                PuppetBehav.collisionResistance = _oldCollisionResistance;
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

            string randomRoll = RandomTricks.GetRandomRoll();///
            animator.CrossFadeInFixedTime(randomRoll, 0.1f);
        }

        public override void Jump()
        {
            if (customAction) return;

            // know if has enough stamina to make this action
            bool staminaConditions = currentStamina > jumpStamina;
            // conditions to do this action
            bool jumpConditions = !isCrouching && isGrounded && !actions && staminaConditions && !isJumping;
            // return if jumpCondigions is false
            if (!jumpConditions) return;
            // trigger jump behaviour
            jumpCounter = jumpTimer;
            isJumping = true;

            jumpForward = isSprinting ? freeSpeed.sprintSpeed : freeSpeed.runningSpeed;

            // trigger jump animations
            if (input.sqrMagnitude < 0.1f)
                animator.CrossFadeInFixedTime("Jump", 0.1f);//TODO случайная анимация прыжка (?)
            else
                animator.CrossFadeInFixedTime("JumpMove", .2f);
            // reduce stamina
            ReduceStamina(jumpStamina, false);
            currentStaminaRecoveryDelay = 1f;
        }
    }
}
