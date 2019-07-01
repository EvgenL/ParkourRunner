using UnityEngine;
using UnityEngine.SceneManagement;
using AEngine;

public class NextGameTransition : MonoBehaviour
{
    [SerializeField] private MenuTransition _baseTransition;
    [SerializeField] private int _gameSceneID;

    #region Events
    public void OnTransitionButtonClick()
    {
        EnvironmentController.CheckKeys();

        int maxLevel = PlayerPrefs.GetInt(EnvironmentController.MAX_LEVEL);
        int level = PlayerPrefs.GetInt(EnvironmentController.LEVEL_KEY);
        
        bool isBaseLevels = PlayerPrefs.GetInt(EnvironmentController.ENDLESS_KEY) == 0 && PlayerPrefs.GetInt(EnvironmentController.TUTORIAL_KEY) == 0;

        if (isBaseLevels && level <= maxLevel)
        {
            AudioManager.Instance.PlaySound(Sounds.Tap);

            level = Mathf.Clamp(level + 1, 1, maxLevel);

            PlayerPrefs.SetInt(EnvironmentController.LEVEL_KEY, level);
            PlayerPrefs.Save();

            SceneManager.LoadScene(_gameSceneID);
        }
        else
            _baseTransition.OnTransitionButtonClick();
    }
    #endregion
}
