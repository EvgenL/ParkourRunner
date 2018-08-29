using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enemy;
using UnityEngine;

public enum BotType
{
    Laser,
    Bomber,
    Miner,
    Builder
}
public enum BotState
{
    Stay,
    Enter,
    Follow,
    Attack
}

public class EnemyBotAi : EnemyBotController
{
    public BotType BotType;

    private int _difficulty;

    public bool ReadyToFire;

    private Weapon[] _weapons;

    private void Start()
    {
        print("Bot spawned");
       // BotState = BotState.Stay;
    }

    public void StartBattle(Transform player, int difficulty)
    {
        _weapons = GetComponentsInChildren<Weapon>();
        Player = player;
        _difficulty = difficulty;
        State = BotState.Enter;
        //Invoke("Enter", 0.1f);
        print("bot StartBattle.");
        Reload();
    }

    //private void Enter()
    //{
    //    BotState = BotState.Enter;
    //}

    private void Update()
    {
        if (State == BotState.Enter)
        {
            if (Vector3.Distance(_targetPosition, transform.position) < _maxSpeed) 
            {
                State = BotState.Follow;
            }
        }
    }

    public void AttackRandom()
    {
        ReadyToFire = false;
        int randN = Random.Range(0, _weapons.Length);
        _weapons[randN].Attack(Player, _difficulty);
    }

    public void Reload()
    {
        float _reloadTime = 3;
        Invoke("Ready", _reloadTime);
    }

    private void Ready()
    {
        ReadyToFire = true;
        print("Reloaded");
    }
}
