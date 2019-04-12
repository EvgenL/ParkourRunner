using System.Collections;
using ParkourRunner.Scripts.Managers;
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

        private ParkourCamera _pCamera;

        private void Awake()
        {
            _pCamera = ParkourCamera.Instance;
        }

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
            _pCamera.OnLoseBalance();
            InvectorInput.SetLockBasicInput(true);
        }

        public void OnRegainBalance()
        {
            _pCamera.OnRegainBalance();
            Invoke("Unlock", RegainBalanceInputDelay);
            StartCoroutine(RestoreImmuneProcess(GameManager.Instance.RestoreImmuneDuration));
        }

        private void Unlock()
        {
            InvectorInput.SetLockBasicInput(false);
        }

        private IEnumerator RestoreImmuneProcess(float time)
        {
            ParkourThirdPersonController.instance.RestoreImmune = true;

            yield return new WaitForSeconds(time);

            ParkourThirdPersonController.instance.RestoreImmune = false;
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
