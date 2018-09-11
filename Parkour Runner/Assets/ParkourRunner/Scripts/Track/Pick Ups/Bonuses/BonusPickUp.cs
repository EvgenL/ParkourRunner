using ParkourRunner.Scripts.Managers;

namespace ParkourRunner.Scripts.Track.Pick_Ups.Bonuses
{
    public class BonusPickUp : PickUp
    {
        public BonusName BonusName;

        protected override void Pick()
        {
            GameManager.Instance.AddBonus(BonusName);
            Destroy(gameObject);
        }
    }
}
