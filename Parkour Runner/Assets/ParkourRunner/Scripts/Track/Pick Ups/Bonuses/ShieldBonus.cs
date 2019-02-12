using Assets.ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;

namespace ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    class ShieldBonus : Bonus
    {
        protected override void StartEffect()
        {
            base.StartEffect();

            CharacterEffects.Instance.ShieldActive = true;
            _player.Immune = true;
        }

        protected override void EndEffect()
        {
            base.StartEffect();

            CharacterEffects.Instance.ShieldActive = false;
            _player.Immune = false;
        }
    }
}
