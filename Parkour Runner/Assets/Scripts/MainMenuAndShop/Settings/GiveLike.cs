using UnityEngine;
using ParkourRunner.Scripts.Managers;
using AEngine;

public class GiveLike : SettingsBase
{
    public override void OnClick()
    {
        AudioManager.Instance.PlaySound(Sounds.Tap);
        Application.OpenURL(StaticConst.IOS_URL);
    }
}