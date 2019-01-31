using UnityEngine;
using AEngine;

public class GoToHome : MonoBehaviour
{
    public void GoHome()
    {
        SceneLoadManager.Instance.LoadScene("ShopAndMainMenu");
        AudioManager.Instance.PlaySound(Sounds.Tap);        
    }
}
