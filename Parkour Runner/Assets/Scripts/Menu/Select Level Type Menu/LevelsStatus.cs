using UnityEngine;
using UnityEngine.UI;

public class LevelsStatus : MonoBehaviour
{
    [SerializeField] private Text _levelsLabel;
    [SerializeField] private int _maxLevels;

    private void Start()
    {
        EnvironmentController.CheckKeys();

        int openedLevels = Mathf.Clamp(PlayerPrefs.GetInt(EnvironmentController.MAX_LEVEL), 1, _maxLevels);        
        _levelsLabel.text = string.Format("Levels {0}/{1}", openedLevels, _maxLevels);
    }
}