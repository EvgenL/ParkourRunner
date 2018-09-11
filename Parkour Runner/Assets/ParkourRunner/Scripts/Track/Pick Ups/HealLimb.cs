using ParkourRunner.Scripts.Managers;

namespace ParkourRunner.Scripts.Track.Pick_Ups
{
    public class HealLimb : PickUp {

        protected override void Pick()
        {
            if (GameManager.Instance.HealLimb())
            {
                Destroy(gameObject);
            }
        }
    }
}
