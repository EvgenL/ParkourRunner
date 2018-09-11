﻿using System.Collections.Generic;
using Basic_Locomotion.Scripts.CharacterController;
using ParkourRunner.Scripts.Managers;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    class MagnetBonus : MonoBehaviour
    {
        public float TimeRemaining;

        private List<Coin> _coins;

        private Transform _player;
        private ProgressManager _pm;

        void Start()
        {
            _pm = ProgressManager.Instance;
            
            _player = vThirdPersonController.instance.transform;
            _coins = GameManager.Instance.GetCoins();
            RefreshTime();

            //TODO play effect animation
        }

        public void RefreshTime()
        {
            TimeRemaining = _pm.InitialMagnetLength + _pm.MagnetUpgradeLength;
            HUDManager.Instance.UpdateBonus(BonusName.Magnet, 1f);
        }


        void Update()
        {
            TimeRemaining -= Time.deltaTime;
            if (TimeRemaining <= 0f)
            {
                HUDManager.Instance.DisableBonus(BonusName.Magnet);
                Destroy(this);
                return;
            }

            float percent = TimeRemaining / (_pm.InitialMagnetLength + _pm.MagnetUpgradeLength);
            
            HUDManager.Instance.UpdateBonus(BonusName.Magnet, percent);

            foreach (var coin in _coins)
            {
                if (Vector3.Distance(_player.position, coin.transform.position) < _pm.MagnetRadius)
                {
                    var rb = coin.GetComponent<Rigidbody>();
                    if (rb == null)
                    {
                        rb = coin.gameObject.AddComponent<Rigidbody>();
                        rb.useGravity = false;
                    }

                    coin.transform.position = Vector3.MoveTowards(coin.transform.position, 
                        _player.position + Vector3.up, StaticConst.MagnetCoinVelocity * Time.deltaTime);
                }
             }

        }
    }
}