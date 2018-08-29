using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    abstract class Weapon : MonoBehaviour
    {

        [HideInInspector] public EnemyBotController Bot;
        public bool ChangeAttackPosition;
        public Vector3 AttackPosition;
        [SerializeField] protected float MaxAimTime = 1f;
        [SerializeField] protected float MinAimTime = 3f;

        public void Start()
        {
            Bot = GetComponent<EnemyBotController>();
        }

        public abstract void Attack(Transform player, int difficulty);

        protected void Aim()
        {
            StartCoroutine(Aiming());
        }
        protected abstract IEnumerator Aiming();

        }
}
