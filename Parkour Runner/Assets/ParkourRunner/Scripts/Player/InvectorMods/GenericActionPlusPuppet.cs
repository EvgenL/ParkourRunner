using Basic_Locomotion.Scripts.CharacterController.Actions;
using ParkourRunner.Scripts.Managers;
using RootMotion.Dynamics;
using UnityEngine;

namespace ParkourRunner.Scripts.Player.InvectorMods
{

    class GenericActionPlusPuppet : vGenericAction
    {
        public PuppetMaster puppetMaster;
        public BehaviourPuppet behaviourPuppet;
        [SerializeField] private string _randomAnimation;

        

        protected override void ApplyPlayerSettings()
        {
            base.ApplyPlayerSettings();
            //puppetMaster.mode = PuppetMaster.Mode.Disabled;

        }

        protected override void ResetPlayerSettings()
        {
            base.ResetPlayerSettings();
            //puppetMaster.mode = PuppetMaster.Mode.Active;
        }
        protected override void TriggerActionInput()
        {
            if (triggerAction == null) return;
            if (canTriggerAction)
            {
                if (
                    //TODO странно работает
                    (triggerAction.autoAction || actionInput.GetButtonDown())
                    //&& actionConditions
                )
                {
                    if (!triggerActionOnce)
                    {
                        OnDoAction.Invoke(triggerAction);
                        TriggerAnimation();
                        if (triggerAction.playAnimation == "Stand")
                         Destroy(triggerAction.gameObject)
                        ;
                    }
                }
            }
        }
        protected override void TriggerAnimation()
        {
            if (debugMode) Debug.Log("TriggerAnimation");

            // trigger the animation behaviour & match target
            if (!string.IsNullOrEmpty(triggerAction.playAnimation))
            {
                isPlayingAnimation = true;

                _randomAnimation = RandomTricks.GetTrick(triggerAction.playAnimation);

                tpInput.cc.animator.CrossFadeInFixedTime(_randomAnimation, 0.1f); // trigger the action animation clip
            }

            // trigger OnDoAction Event, you can add a delay in the inspector   
            StartCoroutine(triggerAction.OnDoActionDelay(gameObject));

            // bool to limit the autoAction run just once
            if (triggerAction.autoAction || triggerAction.destroyAfter) triggerActionOnce = true;

            // destroy the triggerAction if checked with destroyAfter
            if (triggerAction.destroyAfter)
                StartCoroutine(DestroyDelay(triggerAction));
        }

        protected override bool playingAnimation
        {
            get
            {
                if (triggerAction == null)
                {
                    isPlayingAnimation = false;
                    return false;
                }

                if (!isPlayingAnimation && !string.IsNullOrEmpty(triggerAction.playAnimation) &&
                    tpInput.cc.baseLayerInfo.IsName(_randomAnimation))
                {
                    isPlayingAnimation = true;
                    if (triggerAction != null) triggerAction.OnPlayerExit.Invoke();
                    ApplyPlayerSettings();
                }
                else if (isPlayingAnimation && !string.IsNullOrEmpty(triggerAction.playAnimation) &&
                         !tpInput.cc.baseLayerInfo.IsName(_randomAnimation))
                {
                    isPlayingAnimation = false;
                }

                return isPlayingAnimation;
            }
        }

        public override void OnActionStay(Collider other)
        {
            base.OnActionEnter(other);
            base.OnActionStay(other);
        }

        protected override void AnimationBehaviour()
        {
            if (playingAnimation)
            {
                if (triggerAction.matchTarget != null)
                {
                    if (debugMode) Debug.Log("Match Target...");
                    // use match target to match the Y and Z target 
                    tpInput.cc.MatchTarget(triggerAction.matchTarget.transform.position, triggerAction.matchTarget.transform.rotation, triggerAction.avatarTarget,
                        new MatchTargetWeightMask(triggerAction.matchTargetMask, 0), triggerAction.startMatchTarget, triggerAction.endMatchTarget);
                }

                if (triggerAction.useTriggerRotation)
                {
                    if (debugMode) Debug.Log("Rotate to Target...");
                    // smoothly rotate the character to the target
                    transform.rotation = Quaternion.Lerp(transform.rotation, triggerAction.transform.rotation, tpInput.cc.animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                }

                if (triggerAction.resetPlayerSettings && tpInput.cc.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= triggerAction.endExitTimeAnimation)
                {
                    if (debugMode) Debug.Log("Finish Animation");
                    // after playing the animation we reset some values
                    ResetPlayerSettings();
                }
            }
        }

        void LateUpdate()
        {
            if (playingAnimation)
            {
                if (triggerAction.useTriggerRotation)
                {
                    if (debugMode) Debug.Log("Rotate to Target...");
                    // smoothly rotate the character to the target
                    //transform.rotation = Quaternion.identity;
                    transform.rotation = Quaternion.Lerp(transform.rotation, triggerAction.transform.rotation, tpInput.cc.animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                }
            }
        }

    }
}
