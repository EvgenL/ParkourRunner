using UnityEngine;
using AEngine;

public class CharacterSettingButton : SettingsBase
{
    [SerializeField] private MenuController _menuController;
    
    public override void OnClick()
    {
        AudioManager.Instance.PlaySound(Sounds.Tap);
        _menuController.OpenMenu(MenuKinds.Characters);
    }
}
