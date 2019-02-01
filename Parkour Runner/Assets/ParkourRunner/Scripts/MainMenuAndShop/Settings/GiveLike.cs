using AEngine;

public class GiveLike : SettingsBase
{
    public override void OnClick()
    {
        AudioManager.Instance.PlaySound(Sounds.Tap);
    }
}
