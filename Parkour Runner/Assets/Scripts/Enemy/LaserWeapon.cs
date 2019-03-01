using System;
using System.Collections;
using ParkourRunner.Scripts.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ParkourRunner.Scripts.Enemy
{
    enum LaserAttackType
    {
        SingleFront,
        WallVertical,
        WallVerticalStay,
        WallTop,
        WallBottom
    }

    class LaserWeapon : Weapon
    {

        public GameObject SingleLaser;
        public GameObject LaserWallVertical;
        public GameObject LaserWallTop;
        public GameObject LaserWallBottom;

        public float AttackHeight;

        private Transform _player;
        private int _difficulty;


        private LaserAttackType AttackType;

        public override void Attack(Transform player, int difficulty)
        {
            _difficulty = difficulty;
            _player = player;

            //TODO учитывать сложность
            //Get random enum
            AttackType = (LaserAttackType)Random.Range(0, Enum.GetValues(typeof(LaserAttackType)).Length);
            print("AttackType " + AttackType);
            Aim();
        }

        protected override IEnumerator Aiming()
        {
            Bot.ChangeAttackSpeed = true;
            Bot.AttackSpeed = 4f;

            float aimTime = Utility.MapValue(_difficulty, 0, StaticConst.MaxEnemyDifficulty, MaxAimTime, MinAimTime);
            print("Aiming " + aimTime);

            Transform limb = GameManager.Instance.GetRandomLimb();
            print("limb " + limb);

            float time = 0;
            while (time <= aimTime)
            {

                Bot.ChangeAttackHeight = true;
                if (AttackType == LaserAttackType.WallVerticalStay)
                {
                    Bot.AttackHeight = limb.position.y;
                }
                else
                {
                    Bot.AttackHeight = AttackHeight;
                }

                time += Time.deltaTime;
                yield return null;
            }

            if (AttackType == LaserAttackType.WallVerticalStay)
            {
                StartCoroutine(FiringWallStay());
            }
            else if(AttackType == LaserAttackType.SingleFront)
            {
                StartCoroutine(FiringSingleFront());
            }
            {
                StartCoroutine(FiringFollow());
            }
        }
        private IEnumerator FiringSingleFront()
        {
            SingleLaser.SetActive(true);

            yield return new WaitForSeconds(2f);

            Bot.ChangeAttackSpeed = false;
            Bot.ChangeAttackHeight = false;

            SingleLaser.SetActive(false);
            LaserWallVertical.SetActive(false);
            LaserWallTop.SetActive(false);
            LaserWallBottom.SetActive(false);
            Reload();
        }

        private IEnumerator FiringFollow()
        {
            switch (AttackType)
            {
                case LaserAttackType.WallVertical:
                    LaserWallVertical.SetActive(true);
                    break;
                case LaserAttackType.WallTop:
                    LaserWallTop.SetActive(true);
                    break;
                case LaserAttackType.WallBottom:
                    LaserWallBottom.SetActive(true);
                    break;
            }


            yield return new WaitForSeconds(0.5f);

            Bot.ChangeAttackSpeed = false;
            Bot.ChangeAttackHeight = false;

            SingleLaser.SetActive(false);
            LaserWallVertical.SetActive(false);
            LaserWallTop.SetActive(false);
            LaserWallBottom.SetActive(false);
            Reload();
        }

        private IEnumerator FiringWallStay()
        {
            Bot.ChangeAttackHeight = true;
            Bot.AttackHeight = 1.8f;

            Bot.ChangeAttackSpeed = true;
            Bot.AttackSpeed = 0.5f;
            LaserWallVertical.SetActive(true);

            yield return new WaitForSeconds(2.5f);

            Bot.ChangeAttackSpeed = false;
            Bot.ChangeAttackHeight = false;

            LaserWallVertical.SetActive(false);
            Reload();
        }

        private void Reload()
        {
            var bot = GetComponent<EnemyBotAi>();
            bot.Reload(); //count
        }
    }
}
