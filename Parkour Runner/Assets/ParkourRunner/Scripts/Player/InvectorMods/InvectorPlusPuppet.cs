using System.Collections;
using Basic_Locomotion.Scripts.CharacterController;
using RootMotion.Dynamics;
using UnityEngine;

namespace ParkourRunner.Scripts.Player.InvectorMods
{
    public class InvectorPlusPuppet : MonoBehaviour
    {

        public float VerticalVelocityToRoll = 12;
        public float VerticalVelocityToUnpin = 20;


        public vThirdPersonInput InvectorInput;
        public Animator AnimationController;

        public BehaviourPuppet Puppet;
    
        void Start()
        {
            StartCoroutine(LateStart());
        }

        IEnumerator LateStart()
        {
            yield return new WaitForEndOfFrame();
            AnimationController.updateMode = AnimatorUpdateMode.Normal; //Потому что пуппет мастер ставит mode AnimatePhysics, и сам же портит движение
        }

        public void OnLoseBalance()
        {
            InvectorInput.SetLockBasicInput(true);
        }

        public void OnRegainBalance()
        {
            InvectorInput.SetLockBasicInput(false);
        }

        void Update()
        {
            if (InvectorInput.cc.landHigh)
            {
                //TODO Get Damage
            }
            else if (!InvectorInput.cc.isGrounded && Mathf.Abs(InvectorInput.cc.verticalVelocity) > VerticalVelocityToUnpin)
            {
                //TODO unpin в полёте
                //Puppet.SetState(BehaviourPuppet.State.Unpinned);
            }
        }

    }
}
