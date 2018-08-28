using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Player.InvectorMods;
using Invector.CharacterController;
using UnityEngine;

public class EnemyBattleManager : MonoBehaviour
{
    //Constant
    [SerializeField] private GameObject EnemyBotPrefab;
    [SerializeField] private float _spawnBackOffsetFromPlayer;
    [SerializeField] private bool DEBUG_SpawnOnStart;
    private Transform _player;

    //Per run
    public int BattlesDone; //Сколько боёв игрок прошёл за этот забег

    //Per battle
    public bool PlayerGotHit;
    private List<EnemyBotAi> _bots;

    //TODO debug
    private void Start(){if (DEBUG_SpawnOnStart)StartBattle();}

    public void StartBattle()
    {
        //бля нулл референс опять
        //_player = vThirdPersonController.instance.transform;
        _player = FindObjectOfType<ParkourThirdPersonController>().transform;
        SpawnBots();
        PlayerGotHit = false;


    }

    private void SpawnBots()
    {
        _bots = new List<EnemyBotAi>();
        if (BattlesDone < 5)
        {
            var bot = Instantiate(EnemyBotPrefab, _player.position - (new Vector3(0f, 5f, -_spawnBackOffsetFromPlayer)), Quaternion.identity);
            var botScript = bot.GetComponent<EnemyBotAi>();
            _bots.Add(botScript);
            botScript.StartBattle(_player, Math.Max(BattlesDone, StaticParameters.MaxEnemyDifficulty));
        }
    }
}
