using System;
using System.Collections;
using System.Collections.Generic;
using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Player.InvectorMods;
using UnityEngine;

namespace ParkourRunner.Scripts.Enemy
{
    enum FormationType
    {
        Group, //Все роботы атакуют вместе
        Distinct //Ве роторы атакуют отдельно
    }

    enum WeaponType
    {
        Laser,
        Bomb,
        Minefield
    }

    public class EnemyBattleManager : MonoBehaviour
    {
        //Constant
        [SerializeField] private GameObject LaserBotPrefab;
        [SerializeField] private GameObject BombBotPrefab;
        [SerializeField] private GameObject MinePrefab;
        [SerializeField] private GameObject BuilderBotPrefab;
        [SerializeField] private GameObject BallBotPrefab;
        [SerializeField] private float _spawnBackOffsetFromPlayer;
        [SerializeField] private bool DEBUG_SpawnOnStart;
        [SerializeField] private float _entryWait = 5f;
        [SerializeField] private float _reloadWait = 2f;
        private Transform _player;

        //Per run
        public int BattlesDone; //Сколько боёв игрок прошёл за этот забег

        //Per battle
        public bool PlayerGotHit;
        private List<EnemyBotAi> _bots;
        private bool _isAttacking;

        //TODO debug
        private void Start()
        {
            //бля нулл референс опять
            //_player = vThirdPersonController.instance.transform;
            _player = FindObjectOfType<ParkourThirdPersonController>().transform;

            if (DEBUG_SpawnOnStart) StartBattle();
        }

        public void StartBattle()
        {
            SpawnBots();
            PlayerGotHit = false;
            StartCoroutine(ControllBots());
        }

        private IEnumerator ControllBots()
        {
            print("Bots spawned. Entry wait " + _entryWait + " sec");
            yield return new WaitForSeconds(_entryWait); //Задержка при входе

            while (true)
            {
                if (EverybodyReady())
                {
                    print("EverybodyReady. BotsFire");
                    BotsFire();
                    //TODO не-одновременная атака ботов
                }
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void BotsFire()
        {
            if (_bots.Count == 1)
            {
                _bots[0].AttackRandom();
            }
            foreach (var bot in _bots)
            {
                //TODO выбор типов атаки тут
                bot.AttackRandom();
            }
        }

        private bool EverybodyReady()
        {
            foreach (var bot in _bots)
            {
                if (!bot.ReadyToFire) return false;
            }

            return true;
        }

        private void SpawnBots()
        {
            _bots = new List<EnemyBotAi>();
            if (BattlesDone < 5)
            {
                //TODO случайный боты, зависимость от количества проведённых битв
                var bot = Instantiate(LaserBotPrefab, _player.position - (new Vector3(0f, 5f, _spawnBackOffsetFromPlayer)), Quaternion.identity);
                var botScript = bot.GetComponent<EnemyBotAi>();
                _bots.Add(botScript);

                ////////////////////////////////////Min
                botScript.StartBattle(_player, Math.Max(BattlesDone, StaticConst.MaxEnemyDifficulty));
            }
        }
    }
}