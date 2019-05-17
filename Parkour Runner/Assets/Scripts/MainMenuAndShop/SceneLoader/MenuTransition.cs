using UnityEngine;
using UnityEngine.SceneManagement;
using AEngine;

public class MenuTransition : MonoBehaviour
{
    [SerializeField] private MenuKinds _menuTarget;
    [SerializeField] private string _sceneTarget;

    #region Events
    public void OnTransitionButtonClick()
    {
        AudioManager.Instance.PlaySound(Sounds.Tap);

        MenuController.TransitionTarget = _menuTarget;
        SceneManager.LoadScene(_sceneTarget);
    }
    #endregion
}
