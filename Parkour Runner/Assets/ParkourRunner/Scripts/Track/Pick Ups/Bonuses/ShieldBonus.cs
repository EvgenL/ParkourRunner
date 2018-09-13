using Assets.ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Player.InvectorMods;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    class ShieldBonus : Bonus
    {
        protected override void EndEffect()
        {
            _player.Immune = false;
        }

        protected override void UpdateEffect(float timeRemaining)
        {
            _player.Immune = true;
        }
    }
}
