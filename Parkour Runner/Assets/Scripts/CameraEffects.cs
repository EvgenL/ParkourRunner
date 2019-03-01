using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public static CameraEffects Instance;

    public ParticleSystem MotionSpeedEffect;
    public bool IsRunningFast;
    public bool IsHighJumping;

    private void Start()
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
            {
                MotionSpeedEffect.Stop();
            }
        }
    }
}
