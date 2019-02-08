using UnityEngine;

public class CharacterEffects : MonoBehaviour
{
    public static CharacterEffects Instance;
        
    public ParticleSystem JumpL;
    public ParticleSystem JumpR;

    public ParticleSystem Magnet;
    public ParticleSystem Shield;
    public ParticleSystem Double;

    public bool JumpActive;
    public bool MagnetActive;
    public bool DoubleActive;
    public bool ShieldActive;

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

    void Update ()
	{
	    if (JumpActive)
	    {
	        if (!JumpL.isPlaying)
	        {
	            JumpL.Play();
	            JumpR.Play();
	        }
	    }
	    else
	    {
	        if (JumpL.isPlaying)
	        {
	            JumpL.Stop();
	            JumpR.Stop();
	        }
	    }


	    if (MagnetActive)
	    {
	        if (!Magnet.isPlaying)
	        {
	            Magnet.Play();
	        }
	    }
	    else
	    {
	        if (Magnet.isPlaying)
	        {
	            Magnet.Stop();
	        }
	    }


	    if (ShieldActive)
	    {
	        if (!Shield.isPlaying)
	        {
	            Shield.Play();
	        }
	    }
	    else
	    {
	        if (Shield.isPlaying)
	        {
	            Shield.Stop();
	        }
	    }

	    if (DoubleActive)
	    {
	        if (!Double.isPlaying)
	        {
	            Double.Play();
	        }
	    }
	    else
	    {
	        if (Double.isPlaying)
	        {
	            Double.Stop();
	        }
	    }
    }
}
