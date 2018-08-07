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

        public float CurrentTranslationTime;

        public bool Debug_NotRandomAnimation = false;
        public TrickNames.Roll Debug_Roll;
        public TrickNames.Jump Debug_Jump;
        public TrickNames.ClimbUpClose Debug_ClimbUpClose;
        public TrickNames.ClimbUpFar Debug_ClimbUpFar;
        public TrickNames.JumpOverClose Debug_JumpOverClose;
        public TrickNames.JumpOverFar Debug_JumpOverFar;

        [SerializeField]
        private string _randomAnimation;

        protected override void ApplyPlayerSettings()
        {
            base.ApplyPlayerSettings();
            if (puppetMaster.mode != PuppetMaster.Mode.Disabled)
                puppetMaster.mode = PuppetMaster.Mode.Disabled;
        }
        protected override void ResetPlayerSettings()
        {
            base.ResetPlayerSettings();

            if (puppetMaster.mode != PuppetMaster.Mode.Active) //Функция ResetPlayerSettings вызывается месколько раз, но есть только один кадр, когда эта функция вызвана и canTriggerAction==false
                puppetMaster.mode = PuppetMaster.Mode.Active;
        }
        
        //TODO endexittime = animation.length
        protected override void TriggerAnimation()
        {
            if (debugMode) Debug.Log("TriggerAnimation");

            // trigger the animation behaviour & match target
            if (!string.IsNullOrEmpty(triggerAction.playAnimation))
            {


                isPlayingAnimation = true;

                if (!Debug_NotRandomAnimation)
                    _randomAnimation = Tricks.GetTrick(triggerAction.playAnimation);
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
        
    }
}
