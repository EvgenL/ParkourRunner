using Assets.ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using ParkourRunner.Scripts.Managers;

namespace ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    class JumpBonus : Bonus
    {
        private float _oldJumpHeight;

        private bool IsActive = false;

        protected override void EndEffect()
        {
            base.EndEffect();

            _player.jumpHeight = _oldJumpHeight;
            CharacterEffects.Instance.JumpActive = false;

            IsActive = false;
        }

        protected override void StartEffect()
        {
            if (IsActive)
                return;

            base.StartEffect();

            IsActive = true;

            CharacterEffects.Instance.JumpActive = true;

            _oldJumpHeight = _player.jumpHeight;
            _player.jumpHeight = StaticConst.InitialJumpBonusHeight;
        }
    }
}
