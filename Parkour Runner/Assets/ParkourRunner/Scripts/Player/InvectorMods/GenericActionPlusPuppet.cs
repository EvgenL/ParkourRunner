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

        //public bool ImmuneRagdoll;

        public bool Debug_NotRandomAnimation = false;
        public TrickNames.Roll Debug_Roll;
        public TrickNames.Slide Debug_Slide;
        public TrickNames.JumpOverFar Debug_JumpOverFar;
        public TrickNames.Stand Debug_Stand;

        [SerializeField] private string _randomAnimation;

        protected override void ApplyPlayerSettings()
        {
            base.ApplyPlayerSettings();
            puppetMaster.mode = PuppetMaster.Mode.Disabled;
            GameManager.Instance.PlayerCanBeDismembered = false;
        }

        protected override void ResetPlayerSettings()
        {
            base.ResetPlayerSettings();
            puppetMaster.mode = PuppetMaster.Mode.Active;
            GameManager.Instance.PlayerCanBeDismembered = true;
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

                if (!Debug_NotRandomAnimation)
                    _randomAnimation = RandomTricks.GetTrick(triggerAction.playAnimation);
                else
                    _randomAnimation = GetNotRandomAnimation(triggerAction.playAnimation);


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

        private string GetNotRandomAnimation(string playAnimation)
        {
            switch (playAnimation)
            {

                case ("Roll"):
                    return Debug_Roll.ToString();

                case ("Stand"):
                    return Debug_Stand.ToString();

                default: return "";
            }

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
}
}
