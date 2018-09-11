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
                //Будем делать трюк правой 
                animator.SetBool("MirrorHands", true);
            }
            else
            {
                animator.SetBool("MirrorHands", false);
            }
        }
    }
}
