using System.Collections;
using Basic_Locomotion.Scripts.CharacterController;
using Basic_Locomotion.Scripts.CharacterController.Actions;
using ParkourRunner.Scripts.Managers;
using RootMotion.Dynamics;
using UnityEngine;

namespace ParkourRunner.Scripts.Player.InvectorMods
{
    public class ParkourThirdPersonController : vThirdPersonController
    {
        public BehaviourPuppet BehavPuppet;
        public PuppetMaster PuppetMaster;
        public Weight RollPuppetCollisionResistance;

        public new static ParkourThirdPersonController instance;

        private new void Awake()
        {
            base.Awake();
            instance = this;
        }

        public bool IsOnJumpPlatform;

        public bool IsSlidingDown = false;
        public bool IsSlidingTrolley = false;
        public bool IsRunningWall = false;
        public bool IsUsingHook = false;

        public bool Immune = false;

        public float RollKnockOutDistance = 4f;
        public float _oldKnockOutDistance;

        public float HookSpeed = 2f;

        public float CurrRunSpeed;

        public float CurrAnimSpeed;

        public float SpeedMult = 1f;

        [HideInInspector] public Vector3 TrolleyOffset;
        [HideInInspector] public Vector3 WallOffset;
        [HideInInspector] public Vector3 HookOffset;

        [HideInInspector] public Transform TargetTransform;

        private float _oldSpeed;

        private bool _airSpeedFreeze = false;

        //Чисто по приколу сделал чтоб он держался за IK пока едет на тарзанке
        [HideInInspector] public AvatarIKGoal TrolleyHand;
        void OnAnimatorIK(int layerIndex)
        {
            if (!IsSlidingTrolley) return;

            animator.SetIKPositionWeight(TrolleyHand, 0.7f);
            animator.SetIKPosition(TrolleyHand, TargetTransform.position);
        }

        private GameManager _gm;

        private LayerMask _damageLayers;
        [SerializeField] private LayerMask _immuneLayers = new LayerMask();
        
        private LayerMask _oldCollisions;
        private Weight _oldCollisionResistance;
        //private ParkourThirdPersonInput parkourInput;
        
        private new void Start()
        {
            _damageLayers = LayerMask.NameToLayer("HouseWall");
            _gm = GameManager.Instance;
            ResetSpeed();

            base.Start();
            //parkourInput = GetComponent<ParkourThirdPersonInput>();

            //почему то туда нельзя добавить ивент из этого класса, можно только из родительского
            BehavPuppet.onLoseBalance.unityEvent.AddListener(delegate {
                IsSlidingTrolley = false;
                _capsuleCollider.isTrigger = false;
            });
            BehavPuppet.onLoseBalance.unityEvent.AddListener(ResetSpeed);

            _damageLayers = BehavPuppet.collisionLayers;
        }

        private void ResetSpeed()
        {
            CurrRunSpeed = StaticConst.MinRunSpeed;
            CurrAnimSpeed = StaticConst.MinAnimSpeed;
        }

        private void Update()
        {
            CheckImmunity();
            ControllStates();
            ControllSpeed();

        }

        private void CheckImmunity()
        {
            if (customAction || Immune)
            {
                BehavPuppet.collisionLayers = _immuneLayers;
                _gm.PlayerCanBeDismembered = false;
                //PuppetMaster.mode = PuppetMaster.Mode.Kinematic;
            }
            else
            {
                BehavPuppet.collisionLayers = _damageLayers;
                _gm.PlayerCanBeDismembered = true;
                //PuppetMaster.mode = PuppetMaster.Mode.Active;
            }
        }

        private void ControllSpeed()
        {
            if (!_airSpeedFreeze) 
            {
                freeSpeed.runningSpeed = CurrRunSpeed * SpeedMult;
                freeSpeed.walkSpeed = CurrRunSpeed;
                animator.SetFloat("TrickSpeedMultiplier", CurrAnimSpeed);

                CurrRunSpeed = Utility.MapValue(_gm.GameSpeed,
                    1f, StaticConst.MaxGameSpeed, StaticConst.MinRunSpeed, StaticConst.MaxRunSpeed);
                CurrAnimSpeed = Utility.MapValue(_gm.GameSpeed,
                    1f, StaticConst.MaxGameSpeed, StaticConst.MinAnimSpeed, StaticConst.MaxAnimSpeed);
            }
        }

        private void FixedUpdate()
        {
            if (IsSlidingTrolley)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, TargetTransform.position + TrolleyOffset, 0.1f);

                Quaternion newRot = TargetTransform.rotation;
                newRot.x = 0;
                newRot.z = 0;
                transform.rotation = newRot;

                PuppetMaster.mode = PuppetMaster.Mode.Active;

                return;
            }
            if (IsRunningWall)
            {
                var newPos = Vector3.Lerp(transform.position, TargetTransform.position + WallOffset, 0.5f);
                transform.position = newPos;
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

        }

        private void ControllStates()
        {
            animator.SetBool("IsRunningWall", IsRunningWall);
            animator.SetBool("IsSlidingDown", IsSlidingDown);
            animator.SetBool("IsSlidingTrolley", IsSlidingTrolley);
            animator.SetBool("IsUsingHook", IsUsingHook);
        }

        public new bool actions
        {
            get { return /*isRolling ||*/ quickStop || landHigh || customAction; }
        }
        public override void Roll()
        {
            bool staminaCondition = currentStamina > rollStamina;
            // can roll even if it's on a quickturn or quickstop animation
            bool actionsRoll = !actions || (actions && (quickStop));
            // general conditions to roll
            bool rollConditions = (input != Vector2.zero || speed > 0.25f) && actionsRoll && isGrounded && staminaCondition && !isJumping;

            if (!rollConditions || isRolling) return;

            string randomRoll = RandomTricks.GetRandomRoll();//я добавил
            animator.CrossFadeInFixedTime(randomRoll, 0.1f);
            _capsuleCollider.isTrigger = false;
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
                animator.CrossFadeInFixedTime("Jump", 0.1f);
            else
                animator.CrossFadeInFixedTime("JumpMove", .2f);
            // reduce stamina
            ReduceStamina(jumpStamina, false);
            currentStaminaRecoveryDelay = 1f;
        }

        public void Die()
        {
            PuppetMaster.state = PuppetMaster.State.Dead;
            PuppetMaster.muscles[0].rigidbody.AddForce(_rigidbody.velocity); //толкаем таз скоростью капсулы
        }
        public void Revive()
        {
            PuppetMaster.state = PuppetMaster.State.Alive;
        }


        public virtual void PlatformJump(float speed, float height)
        {
            jumpCounter = jumpTimer;
            isJumping = true;
            // trigger jump animations
            animator.CrossFadeInFixedTime("JumpMove", .2f);

            StartCoroutine(FreezeInAir(speed, height));
        }

        //Это нужно чтобы на прыжке с батута всегда была постоянная скорость
        private IEnumerator FreezeInAir(float speed, float height)
        {
            if (_airSpeedFreeze) yield break;
            _airSpeedFreeze = true;

            float oldSpeed = jumpForward;
            float oldHeight = jumpHeight;

            while (isJumping || !isGrounded)
            {
                IsOnJumpPlatform = true;
                freeSpeed.runningSpeed = speed;
                freeSpeed.walkSpeed = speed;
                jumpForward = speed;
                jumpHeight = height;
                yield return null;
            }

            jumpForward = oldSpeed;
            jumpHeight = oldHeight;
            _airSpeedFreeze = false;
            IsOnJumpPlatform = false;
        }

        public void Immunity(float t)
        {
            StartCoroutine(ImmunityTime(t));
        }

        private IEnumerator ImmunityTime(float t)
        {
            while (t > 0)
            {
                t -= Time.deltaTime;
                Immune = true;
                yield return null;
            }
            Immune = false;
        }
    }
}
