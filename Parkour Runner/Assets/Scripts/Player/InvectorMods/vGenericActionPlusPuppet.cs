﻿using System;
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

        //public bool ImmuneRagdoll;

        public bool Debug_NotRandomAnimation = false;
        public TrickNames.Roll Debug_Roll;
        public TrickNames.Slide Debug_Slide;
        public TrickNames.JumpOverClose Debug_JumpOverClose;
        public TrickNames.JumpOverFar Debug_JumpOverFar;
        public TrickNames.Stand Debug_Stand;

        [SerializeField] private string _randomAnimation;

        protected override void ApplyPlayerSettings()
        {
            base.ApplyPlayerSettings();
            puppetMaster.mode = PuppetMaster.Mode.Disabled;
        }

        protected override void ResetPlayerSettings()
        {
            base.ResetPlayerSettings();
            puppetMaster.mode = PuppetMaster.Mode.Active;
        }
        public override bool actionConditions
        {
            get
            {
                /*return !(tpInput.cc.isJumping 
                         //|| tpInput.cc.actions 
                         || !canTriggerAction 
                         //|| isPlayingAnimation
                         ) 
                      // && !tpInput.cc.animator.IsInTransition(0)
                    ;*/
                return true;
            }
        }
        protected override void TriggerActionInput()
        {
            if (triggerAction == null) return;

            if (canTriggerAction)
            {
                if ((triggerAction.autoAction && actionConditions) || (actionInput.GetButtonDown() && actionConditions))
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

                case ("JumpOverClose"):
                    return Debug_JumpOverClose.ToString();

                case ("JumpOverFar"):
                    return Debug_JumpOverFar.ToString();

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
