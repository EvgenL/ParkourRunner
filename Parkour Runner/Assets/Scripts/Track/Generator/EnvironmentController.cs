using UnityEngine;

[CreateAssetMenu(fileName = "Environment Controller", menuName = "ParkouRunner/Environment Controller")]
public class EnvironmentController : ScriptableObject
{
    public const string TUTORIAL_KEY = "Tutorial";
    public const string ENDLESS_KEY = "Endless";
    public const string LEVEL_KEY = "Level";
    public const string MAX_LEVEL = "Max Level";

    [SerializeField] private Environment _tutorial;
    [SerializeField] private Environment[] _levels;

    [Header("Debug mode")]
    [SerializeField] private bool _debug;
    [SerializeField] private Environment _target;

    public static void CheckKeys()
    {
        if (!PlayerPrefs.HasKey(TUTORIAL_KEY) || !PlayerPrefs.HasKey(LEVEL_KEY) || !PlayerPrefs.HasKey(ENDLESS_KEY) || !PlayerPrefs.HasKey(MAX_LEVEL))
        {
            PlayerPrefs.SetInt(TUTORIAL_KEY, 1);
            PlayerPrefs.SetInt(ENDLESS_KEY, 0);
            PlayerPrefs.SetInt(LEVEL_KEY, 1);
            PlayerPrefs.SetInt(MAX_LEVEL, 1);
            PlayerPrefs.Save();
        }
    }

    public Environment GetActualEnvironment()
    {
        CheckKeys();

        if (_debug && _target != null)
            return _target;

        if (PlayerPrefs.GetInt(TUTORIAL_KEY) == 1)
            return _tutorial;
        else if (PlayerPrefs.GetInt(ENDLESS_KEY) == 1)
            return GetEndless();
        else
            return GetLevelByIndex(PlayerPrefs.GetInt(LEVEL_KEY));
    }

    private Environment GetEndless()
    {
        foreach (Environment item in _levels)
            if (item.EndlessLevel)
                return item;

        Debug.LogError("Couldn't find endless level");

        return null;
    }

    private Environment GetLevelByIndex(int index)
    {
        foreach (Environment item in _levels)
            if (item.LevelIndex == index)
                return item;

        Debug.LogError("Couldn't find level by index " + index);

        return null;
    }
}