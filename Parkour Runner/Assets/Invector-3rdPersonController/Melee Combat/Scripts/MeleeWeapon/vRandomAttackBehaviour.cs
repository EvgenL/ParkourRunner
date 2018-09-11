using UnityEngine;

namespace Melee_Combat.Scripts.MeleeWeapon
{
    public class vRandomAttackBehaviour : StateMachineBehaviour
    {
        public int attackCount;

        //OnStateMachineEnter is called when entering a statemachine via its Entry Node
        override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        {
            animator.SetInteger("RandomAttack", Random.Range(0, attackCount));
        }
    }
}