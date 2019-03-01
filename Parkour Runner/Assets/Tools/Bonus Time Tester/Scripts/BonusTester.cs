using ParkourRunner.Scripts.Managers;
using AEngine;
using UnityEngine;

public class BonusTester : MonoBehaviour
{
    public enum TestKinds
    {
        ImplementBonus,
        CheckActiveBonuses
    }

    [SerializeField] private BonusName _bonusName;
    [SerializeField] private TestKinds _actionMode;
        
    void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            switch (_actionMode)
            {
                case TestKinds.ImplementBonus:
                    GameManager.Instance.AddBonus(_bonusName);
                    AudioManager.Instance.PlaySound(Sounds.Bonus);
                    break;

                case TestKinds.CheckActiveBonuses:
                    print("Active bonuses: " + GameManager.Instance.ActiveBonuses.Count);
                    foreach (var item in GameManager.Instance.ActiveBonuses)
                    {
                        print(item);
                    }
                    break;
            }
        }
	}
}
