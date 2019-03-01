namespace Assets.ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    class BoostBonus : Bonus
    {
        protected override void StartEffect()
        {
            base.StartEffect();

            _player.SpeedMult = 1.5f;
            _player.Immune = true;

            CameraEffects.Instance.IsRunningFast = true;
        }
        
        protected override void EndEffect()
        {
            base.EndEffect();

            _player.SpeedMult = 1f;

            if (!_gameManager.ActiveBonuses.Contains(BonusName.Shield))
                _player.Immune = false;

            CameraEffects.Instance.IsRunningFast = false;
        }
    }
}
