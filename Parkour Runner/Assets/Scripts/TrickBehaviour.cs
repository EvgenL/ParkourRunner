using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.InvectorMods;
using Invector.CharacterController;
using Invector.CharacterController.Actions;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class TrickBehaviour : StateMachineBehaviour
{
    public bool OverrideActionParameters = true;


    public bool disableGravity = true;
    public bool disableCollision = true;
    public float exitTime = 0.8f;

    public AvatarTarget avatarTarget;

    public Vector3 matchTargetMask;

    public float startMatchTarget;

    public float endMatchTarget;

    public bool UseRootMotion = true;
    private bool _oldUseRootMotion;

    private vTriggerGenericAction _oldActionParameters = new vTriggerGenericAction();

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        
        if (OverrideActionParameters)
        {
            //Получаем из игрока информацию о триггере, на который он наступил
            var action = vThirdPersonController.instance.GetComponent<vGenericActionPlusPuppet>();
            _oldUseRootMotion = action.useRootMotion;
            action.useRootMotion = UseRootMotion;

            vTriggerGenericAction trigger = action.triggerAction;

            _oldActionParameters.endExitTimeAnimation = trigger.endExitTimeAnimation;
            _oldActionParameters.matchTargetMask = trigger.matchTargetMask;
            _oldActionParameters.avatarTarget = trigger.avatarTarget;
            _oldActionParameters.startMatchTarget = trigger.startMatchTarget;
            _oldActionParameters.endMatchTarget = trigger.endMatchTarget;
            _oldActionParameters.disableGravity = trigger.disableGravity;
            _oldActionParameters.disableCollision = trigger.disableCollision;

            trigger.endExitTimeAnimation = exitTime;
            trigger.matchTargetMask = matchTargetMask;
            trigger.avatarTarget = avatarTarget;
            trigger.startMatchTarget = startMatchTarget;
            trigger.endMatchTarget = endMatchTarget;
            trigger.disableGravity = disableGravity;
            trigger.disableCollision = disableCollision;

        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        var action = vThirdPersonController.instance.GetComponent<vGenericActionPlusPuppet>();
        action.useRootMotion = _oldUseRootMotion;

        vTriggerGenericAction trigger = action.triggerAction;
        trigger.endExitTimeAnimation = _oldActionParameters.endExitTimeAnimation;
        trigger.matchTargetMask = _oldActionParameters.matchTargetMask;
        trigger.avatarTarget = _oldActionParameters.avatarTarget;
        trigger.startMatchTarget = _oldActionParameters.startMatchTarget;
        trigger.endMatchTarget = _oldActionParameters.endMatchTarget;
        trigger.disableGravity = _oldActionParameters.disableGravity;
        trigger.disableCollision = _oldActionParameters.disableCollision;
    }


}
