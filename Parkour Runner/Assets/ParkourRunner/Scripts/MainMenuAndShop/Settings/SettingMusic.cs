using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingMusic : SettingsBase {
    private Image _musicImg;
    [SerializeField] private Sprite[] _musicSprites;
    public override void OnClick()
    {
        if (PlayerPrefs.GetString("MusicOn") == "true")
        {
            PlayerPrefs.SetString("MusicOn", "false");
        }
        else
        {
            PlayerPrefs.SetString("MusicOn", "true");
        }
        UsePlayerPrefs();
    }
    private void UsePlayerPrefs()
    {
        if (PlayerPrefs.GetString("MusicOn") == "true")
        {
            _musicImg.sprite = _musicSprites[0];
        }
        if (PlayerPrefs.GetString("MusicOn") == "false")
        {
            _musicImg.sprite = _musicSprites[1];
        }
    }
    private void SetPlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("MusicOn"))
        {
            PlayerPrefs.SetString("MusicOn", "true");
        }
    }
    private void Start()
    {
        _musicImg = GetComponent<Image>();
        SetPlayerPrefs();
        UsePlayerPrefs();
    }
}
