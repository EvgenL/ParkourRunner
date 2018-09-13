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
        public GameObject Boost;
        public Slider BoostSlider;
        public GameObject DoubleCoins;
        public Slider DoubleCoinsSlider;

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
                case (BonusName.Boost):
                    Boost.SetActive(true);
                    BoostSlider.value = value;
                    break;
                case (BonusName.DoubleCoins):
                    DoubleCoins.SetActive(true);
                    DoubleCoinsSlider.value = value;
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
                case (BonusName.Boost):
                    Boost.SetActive(false);
                    break;
                case (BonusName.DoubleCoins):
                    DoubleCoins.SetActive(false);
                    break;
            }
        }


    }
}
