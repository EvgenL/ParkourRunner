using System.Collections.Generic;
using Assets.ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using Basic_Locomotion.Scripts.CharacterController;
using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Player.InvectorMods;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    class MagnetBonus : Bonus
    {
        
        private List<Coin> _coins;

        private void Start()
        {
            _coins = GameManager.Instance.GetCoins();
            //TODO play effect animation
        }

        protected override void EndEffect()
        {
        }

        protected override void UpdateEffect(float timeRemaining)
        {
            if (_player == null) _player = ParkourThirdPersonController.instance;
            foreach (var coin in _coins)
            {
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
        }
    }
}
