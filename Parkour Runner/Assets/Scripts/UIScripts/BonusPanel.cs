using UnityEngine;
using UnityEngine.UI;

namespace ParkourRunner.Scripts.UIScripts
{
    public class BonusPanel : MonoBehaviour
    {
        public GameObject Magnet;
        public Image MagnetSlider;
        public GameObject Shield;
        public Image ShieldSlider;
        public GameObject Jump;
        public Image JumpSlider;
        public GameObject Boost;
        public Image BoostSlider;
        public GameObject DoubleCoins;
        public Image DoubleCoinsSlider;

        public void UpdateBonus(BonusName bonusName, float value)
        {
            switch (bonusName)
            {
                case (BonusName.Magnet):
                    Magnet.SetActive(true);
                    MagnetSlider.fillAmount = value;
                    break;
                case (BonusName.Jump):
                    Jump.SetActive(true);
                    JumpSlider.fillAmount = value;
                    break;
                case (BonusName.Shield):
                    Shield.SetActive(true);
                    ShieldSlider.fillAmount = value;
                    break;
                case (BonusName.Boost):
                    Boost.SetActive(true);
                    BoostSlider.fillAmount = value;
                    break;
                case (BonusName.DoubleCoins):
                    DoubleCoins.SetActive(true);
                    DoubleCoinsSlider.fillAmount = value;
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
