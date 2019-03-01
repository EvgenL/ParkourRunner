using UnityEngine;

namespace ParkourRunner.Scripts.Player
{
    public class ActivateRagdoll : StateMachineBehaviour {

        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            var behav = FindObjectOfType<PuppetMasterHighFall>();
            behav.DieTemporary();
        }






    }
}
