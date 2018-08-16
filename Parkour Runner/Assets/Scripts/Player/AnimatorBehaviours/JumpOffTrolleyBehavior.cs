﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.InvectorMods;
using Invector.CharacterController;
using UnityEngine;

public class JumpOffTrolleyBehavior : StateMachineBehaviour
{
    private  ParkourThirdPersonController _player;

     public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

	    _player = vThirdPersonController.instance.GetComponent<ParkourThirdPersonController>();
	    _player.IsSlidingTrolley = false;

	    _player._capsuleCollider.isTrigger = false; // disable the collision of the player if necessary 
	    _player._rigidbody.useGravity = true; // disable gravity of the player
    }

}
