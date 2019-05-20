using UnityEngine;
using UnityEngine.UI;

public class DistanceUIController : MonoBehaviour
{
    [SerializeField] private Text _distanceText;
    [SerializeField] private string _captionText;
    [SerializeField] private int _tabSpace;
    [SerializeField] private bool _enableRound;
    [SerializeField] private bool _enableMetres;
    [SerializeField] private bool _spaceBeforeMetres;

    private void OnEnable()
    {
        BuildText();
    }

    private void BuildText()
    {
        string text = string.IsNullOrEmpty(_captionText) ? string.Empty : _captionText;

        for (int i = 0; i < _tabSpace; i++)
            text += " ";

        float distance = PlayerPrefs.GetFloat("DistanceRecord", 0f);

        text += _enableRound ? Mathf.RoundToInt(distance) : distance;
        text += _spaceBeforeMetres && _enableMetres ? " " : string.Empty;
        text += _enableMetres ? "M" : string.Empty;

        _distanceText.text = text;
    }
}
