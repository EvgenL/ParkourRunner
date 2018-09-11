using UnityEngine;

namespace Shooter._3DModels.Weapons.vRifle.Scripts
{
    public class vBotRifleAnimationControl : MonoBehaviour {
        public Animator animator;
        public float pulseSpeed;
        void Start()
        {
            animator = GetComponent<Animator>();
            animator.SetFloat("PulseSpeed", pulseSpeed);
        }
        public void OnChangePowerChanger(float value)
        {
            animator.SetFloat("PowerCharger", value);
        }
    }
}
