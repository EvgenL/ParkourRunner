using Basic_Locomotion.Scripts.CharacterController;
using ParkourRunner.Scripts.Player.InvectorMods;
using UnityEngine;

namespace ParkourRunner.Scripts.Player.AnimatorBehaviours
{
    public class EnablePlayerPhysicsBehaviour : StateMachineBehaviour
    {

        public float Delay ;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            ParkourThirdPersonController _player;
            _player = vThirdPersonController.instance.GetComponent<ParkourThirdPersonController>();

            _player._capsuleCollider.isTrigger = false; // disable the collision of the player if necessary 
            _player._rigidbody.useGravity = true; // disable gravity of the player

        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            if (animatorStateInfo.normalizedTime >= Delay)
                DisablePhysics();
        }

        private void DisablePhysics()
        {
            ParkourThirdPersonController _player;
            _player = vThirdPersonController.instance.GetComponent<ParkourThirdPersonController>();

            _player._capsuleCollider.isTrigger = false; // disable the collision of the player if necessary 
            _player._rigidbody.useGravity = true; // disable gravity of the player
        }
    }
}
