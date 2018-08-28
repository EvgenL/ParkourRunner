using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enemy;
using UnityEngine;

public class EnemyBotAi : EnemyBotController
{
    private int _difficulty;

    private bool _readyToFire;

    private Weapon[] _weapons;

    private void Start()
    {
        print("Bot spawned");
    }

    public void StartBattle(Transform player, int difficulty)
    {
        _weapons = GetComponentsInChildren<Weapon>();
        Player = player;
        _difficulty = difficulty;
        _botState = BotState.Stay;
        Reload();
    }

    private void Update()
    {
        if (Player == null)
        {
            _botState = BotState.Stay;

        }
        else
        {
            _botState = BotState.Follow;
        }

        if (_readyToFire)
        {
            _readyToFire = false;
            Attack();
        }
    }

    public void Attack()
    {
        int randN = Random.Range(0, _weapons.Length - 1);
        print("randN = " + randN);
        print("_weapons[randN] = " + _weapons[randN]);
        _weapons[randN].Attack(Player, _difficulty);
    }

    public void Reload()
    {
        float _reloadTime = 3;
        Invoke("Ready", _reloadTime);
    }

    private void Ready()
    {
        _readyToFire = true;
        print("Reloaded");
    }
}
