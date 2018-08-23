using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Pick_Ups;
using UnityEngine;

public class Coin : PickUp
{
    public int CoinsToAdd = 1;

    protected override void Pick()
    {
        GameManager.Instance.AddCoin(CoinsToAdd);
        GameManager.Instance.Coins.Remove(this);
        Destroy(gameObject);
    }
}
