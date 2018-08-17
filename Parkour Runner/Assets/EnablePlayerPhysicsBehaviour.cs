using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using Assets.Scripts.Player.InvectorMods;
using Invector.CharacterController;
using Invector.CharacterController.Actions;
using UnityEngine;

public class EnablePlayerPhysicsBehaviour : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
     public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    ParkourThirdPersonController _player;
    _player = vThirdPersonController.instance.GetComponent<ParkourThirdPersonController>();

    _player._capsuleCollider.isTrigger = false; // disable the collision of the player if necessary 
    _player._rigidbody.useGravity = true; // disable gravity of the player

	}
}
