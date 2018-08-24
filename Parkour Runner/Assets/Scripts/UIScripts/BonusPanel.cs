using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using Assets.Scripts.Pick_Ups.Effects;
using UnityEngine;
using UnityEngine.UI;

public class BonusPanel : MonoBehaviour
{

    public GameObject Magnet;
    public Slider MagnetSlider;
    public GameObject Shield;
    public Slider ShieldSlider;
    public GameObject Jump;
    public Slider JumpSlider;

    public void UpdateBonus(BonusName bonusName, float value)
    {
        switch (bonusName)
        {
            case (BonusName.Magnet):
                Magnet.SetActive(true);
                print("upd Magnet " + value);
                MagnetSlider.value = value;
                break;
            case (BonusName.Jump):
                Jump.SetActive(true);
                print("upd jump " + value);
                JumpSlider.value = value;
                break;
            case (BonusName.Shield):
                Shield.SetActive(true);
                print("upd Shield " + value);
                ShieldSlider.value = value;
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
