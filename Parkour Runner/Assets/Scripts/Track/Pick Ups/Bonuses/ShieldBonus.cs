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
            base.EndEffect();

            CharacterEffects.Instance.ShieldActive = false;

            if (!_gameManager.ActiveBonuses.Contains(BonusName.Boost))
                _player.Immune = false;
        }
    }
}
