using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using Assets.Scripts.Player.InvectorMods;
using Invector.CharacterController;
using Invector.CharacterController.Actions;
using UnityEngine;

public class TrolleyBehaviour : StateMachineBehaviour
{
    public AvatarIKGoal IKHand;

    ParkourThirdPersonController _player;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_player == null) _player = vThirdPersonController.instance.GetComponent<ParkourThirdPersonController>();

        _player.TrolleyHand = IKHand;

        if (_player.IsSlidingTrolley) return;
        _player.IsSlidingTrolley = true;

        _player._capsuleCollider.isTrigger = true; // disable the collision of the player if necessary 
        _player._rigidbody.useGravity = false; // disable gravity of the player
        _player._rigidbody.velocity = Vector3.zero;

        var trolleyTrigger = _player.GetComponent<vGenericAction>().triggerAction
            .GetComponent<ParkourTriggerTrolleyAction>();

        _player.TrolleyOffset = trolleyTrigger.PlayerOffset;
        _player.TargetTransform = trolleyTrigger.HoldPointTransform;
    }
}
