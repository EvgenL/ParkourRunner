using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour {

    public static CameraEffects Instance;

    public ParticleSystem MotionSpeedEffect;
    public bool IsRunningFast;
    public bool IsHighJumping;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (IsRunningFast || IsHighJumping)
        {
            if (!MotionSpeedEffect.isPlaying)
                MotionSpeedEffect.Play();
        }
        else
        {
            if (MotionSpeedEffect.isPlaying)
                MotionSpeedEffect.Stop();
        }
    }


    public void PlayMotionEffect()
    {
        MotionSpeedEffect.Play();
    }
    public void StopMotionEffect()
    {
        MotionSpeedEffect.Stop();
    }

}
