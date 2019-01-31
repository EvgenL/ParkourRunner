using UnityEngine;
using UnityEngine.SceneManagement;
using AEngine;

public class GoToShopFromPlayScene : MonoBehaviour {

	public void GoToShop()
    {
        SceneManager.LoadScene("ShopAndMainMenu");
        AudioManager.Instance.PlaySound(Sounds.Tap);
        
        UIDoTweener.priority = 0;
    }
  
}
