using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Invector.CharacterController.Actions;
using RootMotion.Dynamics;
using UnityEngine;

namespace Assets.Scripts.Player.InvectorMods
{

    class vGenericActionPlusPuppet : vGenericAction
    {
        public PuppetMaster puppetMaster;
        public BehaviourPuppet behaviourPuppet;

        public bool ImmuneRagdoll;

        public bool Debug_NotRandomAnimation = false;
        public TrickNames.Roll Debug_Roll;
        public TrickNames.Slide Debug_Slide;
        public TrickNames.Jump Debug_Jump;
        public TrickNames.ClimbUpClose Debug_ClimbUpClose;
        public TrickNames.ClimbUpFar Debug_ClimbUpFar;
        public TrickNames.ClimbUp1mClose Debug_ClimbUp1mClose;
        public TrickNames.ClimbUp1mFar Debug_ClimbUp1mFar;
        public TrickNames.JumpOverClose Debug_JumpOverClose;
        public TrickNames.JumpOverFar Debug_JumpOverFar;
        public TrickNames.Stand Debug_Stand;

        [SerializeField]
        private string _randomAnimation;

        protected void FixedUpdate()
        {
            if (ImmuneRagdoll)
            {
                puppetMaster.mode = PuppetMaster.Mode.Disabled;
            }
            else
            {
                puppetMaster.mode = PuppetMaster.Mode.Active;
            }
        }

        protected override void ApplyPlayerSettings()
        {
            base.ApplyPlayerSettings();
            //if (puppetMaster.mode != PuppetMaster.Mode.Disabled)
                puppetMaster.mode = PuppetMaster.Mode.Disabled;
        }
        protected override void ResetPlayerSettings()
        {
            //TODO Многие анимации дергаются по ресету изза того что рут оказывается под землёй во время включения коллизии
            //if (GetComponent<ParkourThirdPersonController>().IsUsingHook) return;
            
            base.ResetPlayerSettings();

            //if (puppetMaster.mode != PuppetMaster.Mode.Active)
                puppetMaster.mode = PuppetMaster.Mode.Active;
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

                tpInput.cc.animator.CrossFadeInFixedTime(_randomAnimation, 0.1f);    // trigger the action animation clip
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

                case ("Jump"):
                    return Debug_Jump.ToString();

                case ("ClimbUpClose"):
                    return Debug_ClimbUpClose.ToString();

                case ("ClimbUpFar"):
                    return Debug_ClimbUpFar.ToString();

                case ("JumpOverClose"):
                    return Debug_JumpOverClose.ToString();

                case ("JumpOverFar"):
                    return Debug_JumpOverFar.ToString();

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

                if (!isPlayingAnimation && !string.IsNullOrEmpty(triggerAction.playAnimation) && tpInput.cc.baseLayerInfo.IsName(_randomAnimation))
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
