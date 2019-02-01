using UnityEngine;
using UnityEngine.UI;
using AEngine;

public class SettingMusic : SettingsBase
{
    [SerializeField] private Image _musicImg;
    [SerializeField] private Sprite _enableState;
    [SerializeField] private Sprite _disableState;

    private AudioManager _audio;

    private void Awake()
    {
        _audio = AudioManager.Instance;
    }

    private void Start()
    {
        _musicImg.sprite = _audio.IsMusic ? _enableState : _disableState;
    }

    public override void OnClick()
    {
        _audio.PlaySound(Sounds.Tap);
        _audio.IsMusic = !_audio.IsMusic;

        _musicImg.sprite = _audio.IsMusic ? _enableState : _disableState;
    }
}
