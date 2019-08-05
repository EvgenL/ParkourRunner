using UnityEngine;
using UnityEngine.UI;

public class LevelsStatus : MonoBehaviour
{
    [SerializeField] private LocalizationComponent _localization;
    [SerializeField] private Text _levelsLabel;
    [SerializeField] private int _maxLevels;

    private void Start()
    {
        EnvironmentController.CheckKeys();

        int openedLevels = Mathf.Clamp(PlayerPrefs.GetInt(EnvironmentController.MAX_LEVEL), 1, _maxLevels);        
        _levelsLabel.text = string.Format("{0} {1}/{2}", _localization.Text, openedLevels, _maxLevels);
    }
}