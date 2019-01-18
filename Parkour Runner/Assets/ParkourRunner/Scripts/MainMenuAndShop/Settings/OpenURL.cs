using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : SettingsBase {

    public override void OnClick()
    {
        Application.OpenURL("https://alivegames.ru/");
    }
}
