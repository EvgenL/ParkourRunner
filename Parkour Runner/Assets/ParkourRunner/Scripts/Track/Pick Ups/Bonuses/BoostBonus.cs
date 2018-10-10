﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Basic_Locomotion.Scripts.CharacterController;
using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Player.InvectorMods;
using ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using UnityEngine;

namespace Assets.ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    class BoostBonus : Bonus
    {
        protected override void EndEffect()
        {
            _player.SpeedMult = 1f;
            _player.Immune = false;
            CameraEffects.Instance.IsRunningFast = true;
        }

        protected override void UpdateEffect(float timeRemaining)
        {
            _player.SpeedMult = 2f;
            _player.Immune = true;
            CameraEffects.Instance.IsRunningFast = false;
            //TODO slow down last two seconds
        }
    }
}
