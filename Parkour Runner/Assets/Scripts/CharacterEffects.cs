using UnityEngine;

public class CharacterEffects : MonoBehaviour
{
    public static CharacterEffects Instance;
        
    public ParticleSystem JumpL;
    public ParticleSystem JumpR;

    public ParticleSystem Magnet;
    public ParticleSystem Shield;
    public ParticleSystem Double;

    private bool _jumpActive;
    private bool _magnetActive;
    private bool _doubleActive;
    private bool _shiedActive;

    public bool JumpActive
    {
        get { return _jumpActive; }
        set
        {
            _jumpActive = value;
            SetParticleState(JumpL, _jumpActive);
            SetParticleState(JumpR, _jumpActive);
        }
    }
    
    public bool MagnetActive
    {
        get { return _magnetActive; }
        set { _magnetActive = value; SetParticleState(Magnet, _magnetActive); }
    }

    public bool DoubleActive
    {
        get { return _doubleActive; }
        set { _doubleActive = value; SetParticleState(Double, _doubleActive); }
    }
    
    public bool ShieldActive
    {
        get { return _shiedActive; }
        set { _shiedActive = value; SetParticleState(Shield, _shiedActive); }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void SetParticleState(ParticleSystem particles, bool isPlaying)
    {
        if (isPlaying)
        {
            if (!particles.isPlaying)
                particles.Play(true);
        }
        else
        {
            if (particles.isPlaying)
                particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
