using AEngine;

public class CharacterSettingButton : SettingsBase
{
    public override void OnClick()
    {
        AudioManager.Instance.PlaySound(Sounds.Tap);
    }
}
