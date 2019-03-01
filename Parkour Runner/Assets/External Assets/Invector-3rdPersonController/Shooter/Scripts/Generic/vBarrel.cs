using System.Collections.Generic;
using Basic_Locomotion.Scripts.CharacterController;
using Basic_Locomotion.Scripts.ObjectDamage;
using UnityEngine;

namespace Shooter.Scripts.Generic
{
    public class vBarrel : MonoBehaviour, vIDamageReceiver
    {
        public float health=10;
        public Transform referenceTransformUP;
        public float maxAngleUp = 90;
        protected bool isBarrelRoll;
        public UnityEngine.Events.UnityEvent onDead;
        public UnityEngine.Events.UnityEvent onBarrelRoll;
        public List<string> acceptableAttacks = new List<string>() { "explosion", "projectile" };
   
        void OnCollisionEnter()
        {
       
            if (!referenceTransformUP) return;
            var angle = Vector3.Angle(referenceTransformUP.up, Vector3.up);
       
            if (angle> maxAngleUp && !isBarrelRoll)
            {
                isBarrelRoll = true;
                onBarrelRoll.Invoke();
            }     
        }
        public void TakeDamage(vDamage damage, bool hitReaction = true)
        {
            if(acceptableAttacks.Contains(damage.attackName))
            {
                if (health > 0)
                    health -= damage.damageValue;
                if (health <= 0)
                {
                    onDead.Invoke();
                }
            }
        }    
    }
}
