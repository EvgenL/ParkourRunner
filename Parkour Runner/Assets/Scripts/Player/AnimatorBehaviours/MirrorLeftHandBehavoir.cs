using UnityEngine;

namespace ParkourRunner.Scripts.Player.AnimatorBehaviours
{
    public class MirrorLeftHandBehavoir : StateMachineBehaviour {

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (animator.GetBool("RightHand") && animator.GetBool("LeftHand"))
            {
                return;
            }

        

            //но она отвалилась
            if (!animator.GetBool("LeftHand"))
            {
                //Будем делать трюк правой рукой
                //avatarTarget = AvatarTarget.RightHand;
                animator.SetBool("MirrorHands", true);
            }
            else
            {
                animator.SetBool("MirrorHands", false);
            }
        }
    }
}
