using Assets.ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using Basic_Locomotion.Scripts.CharacterController;
using ParkourRunner.Scripts.Managers;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    class JumpBonus : Bonus
    {
        private float _oldJumpHeight;

        protected override void EndEffect()
        {
            _player.jumpHeight = _oldJumpHeight;
        }

        protected override void StartEffect()
        {
            _oldJumpHeight = _player.jumpHeight;
            _player.jumpHeight = StaticConst.InitialJumpBonusHeight;
        }
    }
}
