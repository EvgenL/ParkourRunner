using UnityEngine;
using AEngine;

public class GoToHome : MonoBehaviour
{
    public void GoHome()
    {
        MenuController.TransitionTarget = MenuKinds.None;
        SceneLoadManager.Instance.LoadScene("Menu");

        AudioManager.Instance.PlaySound(Sounds.Tap);        
    }
}