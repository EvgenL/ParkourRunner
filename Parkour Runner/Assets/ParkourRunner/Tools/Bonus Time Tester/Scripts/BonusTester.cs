using ParkourRunner.Scripts.Managers;
using AEngine;
using UnityEngine;

public class BonusTester : MonoBehaviour
{
    [SerializeField] private BonusName _bonusName;

    void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            GameManager.Instance.AddBonus(_bonusName);
            AudioManager.Instance.PlaySound(Sounds.Bonus);
        }		
	}
}
