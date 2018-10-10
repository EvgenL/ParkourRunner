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
            CharacterEffects.Instance.ShieldActive = false;
            _player.Immune = false;
        }

        protected override void UpdateEffect(float timeRemaining)
        {
            CharacterEffects.Instance.ShieldActive = true;
            _player.Immune = true;
        }
    }
}
