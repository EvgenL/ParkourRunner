﻿using System.Collections;
using Basic_Locomotion.Scripts.CharacterController;
using RootMotion.Dynamics;
using UnityEngine;

namespace ParkourRunner.Scripts.Player.InvectorMods
{
    public class InvectorPlusPuppet : MonoBehaviour
    {

        public float VerticalVelocityToRoll = 12;
        public float VerticalVelocityToUnpin = 20;
        public float RegainBalanceInputDelay = 0.5f;

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
            ParkourCamera.Instance.ParkourSlowMo.SlowFor(3f);
            InvectorInput.SetLockBasicInput(true);
        }

        public void OnRegainBalance()
        {
            Invoke("Unlock", RegainBalanceInputDelay);
           // ParkourCamera.Instance.ParkourSlowMo.UnSlow();

        }

        private void Unlock()
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
