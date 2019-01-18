using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToShopFromPlayScene : MonoBehaviour {

	public void GoToShop()
    {
        SceneManager.LoadScene("ShopAndMainMenu");
        UIDoTweener.priority = 0;
    }
  
}
