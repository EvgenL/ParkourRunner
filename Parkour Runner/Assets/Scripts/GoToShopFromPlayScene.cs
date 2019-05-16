using UnityEngine;
using UnityEngine.SceneManagement;
using AEngine;

public class GoToShopFromPlayScene : MonoBehaviour
{
    public void GoToShop()
    {
        //MenuController.TransitionTarget = MenuKinds.Shop;
        MenuController.TransitionTarget = MenuKinds.None;
        SceneManager.LoadScene("Menu");

        AudioManager.Instance.PlaySound(Sounds.Tap);

        UIDoTweener.priority = 0;
    }
}