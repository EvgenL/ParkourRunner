using Basic_Locomotion.Scripts.CharacterController;
using Basic_Locomotion.Scripts.ObjectDamage;
using Basic_Locomotion.Scripts.Ragdoll;
using Melee_Combat.Scripts.MeleeWeapon;
using UnityEngine;

namespace Shooter.Scripts.Shooter
{
    public partial class vCollisionMessage : MonoBehaviour, vIAttackReceiver
    {
        public float damageMultiplier = 1f;
        private vCharacter iChar;
        public  vRagdoll ragdoll;
        public void OnReceiveAttack(vDamage damage, vIMeleeFighter attacker)
        {       
            if (ragdoll && !ragdoll.iChar.isDead)
            {
                var _damage = new vDamage(damage);
                var value = (float)_damage.damageValue;
                _damage.damageValue = (int)(value * damageMultiplier);
                vIMeeleFighterHelper.ApplyDamage(ragdoll.gameObject, _damage, attacker);
            }
            else
            {
                if (!iChar) iChar = GetComponentInParent<vCharacter>();
                if(iChar)
                {
                    var _damage = new vDamage(damage);
                    var value = (float)_damage.damageValue;
                    _damage.damageValue = (int)(value * damageMultiplier);
                    iChar.gameObject.ApplyDamage(_damage, attacker);
                }
            }
        }
    }
}
