using UnityEngine;
using AEngine;

public class OpenURL : SettingsBase
{
    public override void OnClick()
    {
        AudioManager.Instance.PlaySound(Sounds.Tap);
        Application.OpenURL("https://alivegames.ru/");
    }
}
