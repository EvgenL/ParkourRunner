using ParkourRunner.Scripts.Managers;

namespace Assets.ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    class DoubleCoinsBonus : Bonus
    {
        protected override void StartEffect()
        {
            base.StartEffect();

            GameManager.Instance.CoinMultipiler = 2;
            CharacterEffects.Instance.DoubleActive = true;
        }

        protected override void EndEffect()
        {
            base.EndEffect();

            GameManager.Instance.CoinMultipiler = 1;
            CharacterEffects.Instance.DoubleActive = false;
        }
    }
}
