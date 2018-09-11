using Basic_Locomotion.Scripts.CharacterController;
using ParkourRunner.Scripts.Player.InvectorMods;
using UnityEngine;

namespace ParkourRunner.Scripts.Player.AnimatorBehaviours
{
    public class JumpOffTrolleyBehavior : StateMachineBehaviour
    {
        private  ParkourThirdPersonController _player;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

            _player = vThirdPersonController.instance.GetComponent<ParkourThirdPersonController>();
            if (!_player.IsSlidingTrolley) return;
         
            _player.IsSlidingTrolley = false;

            _player._capsuleCollider.isTrigger = false; // disable the collision of the player if necessary 
            _player._rigidbody.useGravity = true; // disable gravity of the player
        }

    }
}
