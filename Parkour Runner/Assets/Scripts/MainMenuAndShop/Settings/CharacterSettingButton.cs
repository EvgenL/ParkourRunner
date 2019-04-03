using UnityEngine;
using AEngine;

public class CharacterSettingButton : SettingsBase
{
    [SerializeField] private UIDoTweener _uiController;

    public override void OnClick()
    {
        AudioManager.Instance.PlaySound(Sounds.Tap);
        
        _uiController.OpenCharactersWindow();
    }
}
