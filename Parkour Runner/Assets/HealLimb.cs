using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Pick_Ups;
using UnityEngine;

public class HealLimb : PickUp {

    protected override void Pick()
    {
        if (GameManager.Instance.HealLimb())
        {
            Destroy(gameObject);
        }
    }
}
