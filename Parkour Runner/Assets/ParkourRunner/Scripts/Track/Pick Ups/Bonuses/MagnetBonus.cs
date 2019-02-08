﻿using Assets.ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Player.InvectorMods;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    class MagnetBonus : Bonus
    {
        
        protected override void EndEffect()
        {
            CharacterEffects.Instance.MagnetActive = false;
        }

        protected override void UpdateEffect(float timeRemaining)
        {
            var _coins = PoolManager.Instance.Coins;
            if (_player == null) _player = ParkourThirdPersonController.instance;
            foreach (var coin in _coins)
            {
                if (coin == null) continue; //tODO fix
                if (Vector3.Distance(_player.transform.position, coin.transform.position) < StaticConst.MagnetRadius)
                {
                    var rb = coin.GetComponent<Rigidbody>();
                    if (rb == null)
                    {
                        rb = coin.gameObject.AddComponent<Rigidbody>();
                        rb.useGravity = false;
                    }

                    coin.transform.position = Vector3.MoveTowards(coin.transform.position,
                        _player.transform.position + Vector3.up, StaticConst.MagnetCoinVelocity * Time.deltaTime);
                }
            }
            CharacterEffects.Instance.MagnetActive = true;

        }
    }
}
