using UnityEngine;

namespace ParkourRunner.Scripts.Player.AnimatorBehaviours
{
    public class MirrorLeftLegBehaviour : StateMachineBehaviour {

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.GetBool("LeftLeg") && animator.GetBool("RightLeg"))
            {
                return;
            }

            if (!animator.GetBool("LeftLeg"))
            {
                animator.SetBool("MirriorInjured", true);
            }
            else
            {
                animator.SetBool("MirriorInjured", false);
            }
        }
    }
}
