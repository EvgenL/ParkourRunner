using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Assets.Scripts.Pick_Ups.Effects;
using UnityEngine;
using UnityEngine.UI;

public class BonusPanel : MonoBehaviour
{

    public GameObject Magnet;
    public Slider MagnetValue;
    public GameObject Shield;
    public Slider ShieldValue;
    public GameObject Jump;
    public Slider JumpValue;

    public void UpdateBonus(BonusName bonusName, float value)
    {
        switch (bonusName)
        {
            case (BonusName.Magnet):
                Magnet.SetActive(true);
                MagnetValue.value = value;
                break;
            case (BonusName.Jump):
                Jump.SetActive(true);
                JumpValue.value = value;
                break;
            case (BonusName.Shield):
                Shield.SetActive(true);
                ShieldValue.value = value;
                break;
        }
    }

    public void DisableBonus(BonusName bonusName)
    {
        switch (bonusName)
        {
            case (BonusName.Magnet):
                Magnet.SetActive(false);
                break;
            case (BonusName.Jump):
                Jump.SetActive(false);
                break;
            case (BonusName.Shield):
                Shield.SetActive(false);
                break;
        }
    }


}
