using Basic_Locomotion.Scripts.CharacterController;
using Basic_Locomotion.Scripts.ObjectDamage;
using UnityEngine;

namespace Melee_Combat.Scripts.MeleeWeapon
{
    public interface vIMeleeFighter: vIAttackReceiver
    {
        void OnEnableAttack();

        void OnDisableAttack();

        void ResetAttackTriggers();

        void BreakAttack(int breakAtkID);

        void OnRecoil(int recoilID);

        bool isBlocking { get; }

        bool isAttacking { get; }

        bool isArmed { get; }

        vCharacter character { get; } 
         
        Transform transform { get; }

        GameObject gameObject { get; }
    }

    public interface vIAttackReceiver
    {
        void OnReceiveAttack(vDamage damage, vIMeleeFighter attacker);

    }

    public static class vIMeeleFighterHelper
    {

        /// <summary>
        /// check if gameObject has a <see cref="vIMeleeFighter"/> Component
        /// </summary>
        /// <param name="receiver"></param>
        /// <returns>return true if gameObject contains a <see cref="vIMeleeFighter"/></returns>
        public static bool IsAMeleeFighter(this GameObject receiver)
        {
            return receiver.GetComponent<vIMeleeFighter>() != null;
        }

        public static void ApplyDamage(this GameObject receiver,vDamage damage,vIMeleeFighter attacker)
        {           
            var attackReceiver = receiver.GetComponent<vIAttackReceiver>();
            if (attackReceiver != null) attackReceiver.OnReceiveAttack(damage, attacker);
            else receiver.ApplyDamage(damage);
        }
        /// <summary>
        /// Get <see cref="vIMeleeFighter"/> of gameObject
        /// </summary>
        /// <param name="receiver"></param>
        /// <returns>the <see cref="vIMeleeFighter"/> component</returns>
        public static vIMeleeFighter GetMeleeFighter(this GameObject receiver)
        {
            return receiver.GetComponent<vIMeleeFighter>();
        }
    }
}
