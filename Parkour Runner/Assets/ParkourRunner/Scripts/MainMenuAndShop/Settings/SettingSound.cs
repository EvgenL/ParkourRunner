using UnityEngine;
using UnityEngine.UI;
using AEngine;

public class SettingSound : SettingsBase
{
    [SerializeField] private Image _soundImg;
    [SerializeField] private Sprite _enableState;
    [SerializeField] private Sprite _disableState;

    private AudioManager _audio;

    private void Awake()
    {
        _audio = AudioManager.Instance;
    }

    private void Start()
    {
        _soundImg.sprite = _audio.IsSound ? _enableState : _disableState;
    }

    public override void OnClick()
    {
        _audio.PlaySound(Sounds.Tap);
        _audio.IsSound = !_audio.IsSound;
        
        _soundImg.sprite = _audio.IsSound ? _enableState : _disableState;
    }
}
