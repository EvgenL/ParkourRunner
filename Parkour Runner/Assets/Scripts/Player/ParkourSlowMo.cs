using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ParkourSlowMo : MonoBehaviour
{

    public static ParkourSlowMo Instance;

    [SerializeField] private float SlowTimeScale = 0.3f;
    [SerializeField] private float SlowUpdateRate = 0.005f;
    [SerializeField] private float DefaultfixedDeltaTime = 0.02f;

    private void Awake()
    {
        Instance = this;
    }

    public void Slow()
    {
        Time.timeScale = SlowTimeScale;
        Time.fixedDeltaTime = SlowUpdateRate;
    }

    // Regain balance
    public void UnSlow()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = DefaultfixedDeltaTime;
    }

    // Lose balance
    public void SlowFor(float seconds)
    {
        StartCoroutine(SlForS(seconds));
    }

    private IEnumerator SlForS(float seconds)
    {
        Slow();
        yield return new WaitForSecondsRealtime(seconds);
        UnSlow();
    }

    public void SmoothlyStopTime()
    {
        StopCoroutine("SmoothContinue");
        StartCoroutine("SmoothSlowing");
    }

    private IEnumerator SmoothSlowing()
    {
        Time.fixedDeltaTime = SlowUpdateRate * 2;
        while (Time.timeScale - 0.02f > 0)
        {
            Time.timeScale -= 0.02f;
            yield return null;
        }
        Time.timeScale = 0.0000001f;
    }

    public void SmoothlyContinueTime()
    {
        StopCoroutine("SmoothSlowing");
        StartCoroutine("SmoothContinue");
    }

    private IEnumerator SmoothContinue()
    {
        while (Time.timeScale < 1)
        {
            Time.timeScale += 0.005f;
            yield return null;
        }
        Time.timeScale = 1f;
        Time.fixedDeltaTime = DefaultfixedDeltaTime;

    }

    public void ContinueTime()
    {
        UnSlow();
    }
}