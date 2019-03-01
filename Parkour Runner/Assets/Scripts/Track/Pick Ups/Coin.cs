using System.Net.Mime;
using ParkourRunner.Scripts.Managers;
using UnityEngine;

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
            if (Application.isPlaying)  //Это нужно, чтобы при выключении приложения в эдиторе не получать куду нулл референсов из-за того что poolmanager is destroyed
            {
                PoolManager.Instance.Coins.Remove(this);
                PoolManager.Instance.Remove(gameObject); 
            }
        }

        private void OnDisable()
        {
            OnDestroy();
        }

        private void Awake()
        {
            PoolManager.Instance.Coins.Add(this);
        }


    }
}
