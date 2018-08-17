using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using Assets.Scripts.Player.InvectorMods;
using Invector.CharacterController;
using Invector.CharacterController.Actions;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class HookBehaviour : StateMachineBehaviour {

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ParkourThirdPersonController _player;
        _player = vThirdPersonController.instance.GetComponent<ParkourThirdPersonController>();

        _player._capsuleCollider.isTrigger = true; // disable the collision of the player if necessary 
        _player._rigidbody.useGravity = false; // disable gravity of the player
        _player._rigidbody.velocity = Vector3.zero;


        var trigger = _player.GetComponent<vGenericAction>().triggerAction
            .GetComponent<ParkourTriggerHookAction>();
        _player.HookOffset = trigger.PlayerOffset;

        _player.TargetTransform = trigger.HookTarget;
    }
}
