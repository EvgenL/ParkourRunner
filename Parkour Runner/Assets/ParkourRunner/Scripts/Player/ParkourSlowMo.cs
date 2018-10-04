using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ParkourSlowMo : MonoBehaviour
{

    [SerializeField] private float SlowTimeScale = 0.7f;
    [SerializeField] private float SlowUpdateRate = 0.005f;
    [SerializeField] private float DefaultUpdateRate = 0.02f;

    public void Slow()
    {
        Time.timeScale = SlowTimeScale;
        
        Time.fixedDeltaTime = SlowUpdateRate;
    }
    public void UnSlow()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = DefaultUpdateRate;
    }

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

}
