﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.InvectorMods;
using RootMotion.Dynamics;
using UnityEngine;

public class PuppetMasterHighFall : MonoBehaviour
{
    [HideInInspector]
    public static PuppetMasterHighFall Instance;

    void Start()
    {
        Instance = this;
    }

    public float Delay = 1f;

    public PuppetMaster PuppetMaster;

    public void DieTemporary()
    {
        //PuppetMaster.state = PuppetMaster.State.Dead;
       // Invoke("Revive", Delay);
    }

    private void Revive()
    {
        //PuppetMaster.state = PuppetMaster.State.Alive;
    }

}