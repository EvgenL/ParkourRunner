using ParkourRunner.Scripts.Player;
using UnityEngine;
using AEngine;
using System;

public class Configuration : MonoSingleton<Configuration>
{
    private const string INPUT_MODE_KEY = "InputConfiguration";

    protected override void Init()
    {
        base.Init();

        DontDestroyOnLoad(this);
    }

    public void SaveInputConfiguration (ControlsMode inputMode)
    {
        PlayerPrefs.SetString(INPUT_MODE_KEY, inputMode.ToString());
        PlayerPrefs.Save();        
    }

    public ControlsMode GetInputConfiguration()
    {
        ControlsMode mode = ControlsMode.TiltAndSwipe;

        if (!PlayerPrefs.HasKey(INPUT_MODE_KEY))
        {
            PlayerPrefs.SetString(INPUT_MODE_KEY, mode.ToString());
        }
        else
        {
            mode = (ControlsMode)Enum.Parse(typeof(ControlsMode), PlayerPrefs.GetString(INPUT_MODE_KEY));
        }

        return mode;
    }
}
