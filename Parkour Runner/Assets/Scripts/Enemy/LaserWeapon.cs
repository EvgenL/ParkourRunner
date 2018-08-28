using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    enum LaserAttackType
    {
        Single,
        WallVertical,
        WallTop,
        WallBottom
    }

    class LaserWeapon : Weapon
    {

        public GameObject SingleLaser;
        public GameObject LaserWallVertical;
        public GameObject LaserWallTop;
        public GameObject LaserWallBottom;

        private Transform _player;
        private int _difficulty;

        private float _firingHold = 1f;


        public override void Attack(Transform player, int difficulty)
        {
            _difficulty = difficulty;
            _player = player;
            Aim();
        }

        protected override void Aim()
        {
            //TODO aim types
            float aimTime = Utility.MapValue(_difficulty, 0, StaticParameters.MaxEnemyDifficulty, MaxAimTime, MinAimTime);
            print("Aiming " + aimTime);
            //TODO play anim
            Invoke("Fire", aimTime);

            Transform limb = GameManager.Instance.Limbs.ToList().Find(x => x.Bodypart == (Bodypart.Head)).transform;
            Bot.ChangeAttackHeight = true;
            var limbH = limb.position.y;
            Bot.AttackHeight = limbH;

        }

        private void Fire()
        {
            //TODO Attack types
            print("Firing");
            StartCoroutine(Firing());
            Bot.ChangeAttackSpeed = true;
            Bot.AttackSpeed = 4f;
        }

        private IEnumerator Firing()
        {
            SingleLaser.SetActive(true);

            float time = 0f;
            while (time <= _firingHold)
            {
                time += Time.deltaTime;
                yield return null;
            }

            Bot.ChangeAttackSpeed = false;
            SingleLaser.SetActive(false);

            Reload();
        }

        private void Reload()
        {
            var bot = GetComponent<EnemyBotAi>();
            bot.Reload(); //count
        }
    }
}
