﻿using ParkourRunner.Scripts.Managers;

namespace ParkourRunner.Scripts.Track.Pick_Ups
{
    public class Coin : PickUp
    {
        public int CoinsToAdd = 1;

        protected override void Pick()
        {
            GameManager.Instance.AddCoin(CoinsToAdd);
            PoolManager.Instance.Coins.Remove(this);
            PoolManager.Instance.Remove(gameObject);
        }

        private void OnDestroy()
        {
            PoolManager.Instance.Coins.Remove(this);
        }
        private void Awake()
        {
            PoolManager.Instance.Coins.Add(this);
        }


    }
}
