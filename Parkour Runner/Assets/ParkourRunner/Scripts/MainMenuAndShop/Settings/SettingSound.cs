using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingSound :SettingsBase {
    private Image _soundImg;
    [SerializeField] private Sprite[] _soundSprites;

    public override void OnClick()
    {
        if (PlayerPrefs.GetString("SoundEffectsOn") == "true")
        {
            PlayerPrefs.SetString("SoundEffectsOn", "false");
        }
        else
        {
            PlayerPrefs.SetString("SoundEffectsOn", "true");
        }
        UsePlayerPrefs();
    }

    private void UsePlayerPrefs()
    {
        if (PlayerPrefs.GetString("SoundEffectsOn") == "true")
        {
            _soundImg.sprite = _soundSprites[0];
        }
        if (PlayerPrefs.GetString("SoundEffectsOn") == "false")
        {
            _soundImg.sprite = _soundSprites[1];
        }
    }
    private void SetPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("SoundEffectsOn"))
        {
            PlayerPrefs.SetString("SoundEffectsOn", "true");
        }
    }
    private void Start()
    {
        _soundImg = GetComponent<Image>();
        SetPlayerPrefs();
        UsePlayerPrefs();
    }
}
