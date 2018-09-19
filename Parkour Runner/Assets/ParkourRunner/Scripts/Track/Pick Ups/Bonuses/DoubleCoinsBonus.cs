﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basic_Locomotion.Scripts.CharacterController;
using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using UnityEngine;

namespace Assets.ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    class DoubleCoinsBonus : Bonus
    {
        protected override void EndEffect()
        {
            GameManager.Instance.CoinMultipiler = 1;
        }

        protected override void StartEffect()
        {
            GameManager.Instance.CoinMultipiler = 2;
        }
    }
}