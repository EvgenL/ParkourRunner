using UnityEngine;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _enable;
    [SerializeField] private Sprite _disable;

    public int Level { get { return _level; } }
    public bool IsActive { get; set; }

    private void Start()
    {
        EnvironmentController.CheckKeys();
        this.IsActive = _level <= PlayerPrefs.GetInt(EnvironmentController.MAX_LEVEL);
        _icon.sprite = this.IsActive ? _enable : _disable;
    }
}
