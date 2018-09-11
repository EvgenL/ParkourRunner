using ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using UnityEngine;
using UnityEngine.UI;

namespace ParkourRunner.Scripts.UIScripts
{
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
                    MagnetSlider.value = value;
                    break;
                case (BonusName.Jump):
                    Jump.SetActive(true);
                    JumpSlider.value = value;
                    break;
                case (BonusName.Shield):
                    Shield.SetActive(true);
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
}
