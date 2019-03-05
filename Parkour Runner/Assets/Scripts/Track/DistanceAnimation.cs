using System.Collections;
using ParkourRunner.Scripts.Player.InvectorMods;
using UnityEngine;
using AEngine;

public class DistanceAnimation : MonoBehaviour
{
    public enum AnimationKinds
    {
        Default,
        PlatformUp,
        Bridge
    }

    public enum AnimationTypes
    {
        Default,
        ByZ
    }

    private static float Delay = 0.3f;

    [SerializeField] private AnimationKinds _animationKind;
    [SerializeField] private AnimationTypes _animationType;

    public float ActivationDistance = 10f;

    private Transform _player;
    private Animator animator;
    private AudioManager _audio;

    private void Awake()
    {
        _audio = AudioManager.Instance;
    }

    void Start ()
	{
	    _player = ParkourThirdPersonController.instance.transform;
        animator = GetComponent<Animator>();
	    if (animator == null)
	    {
	        animator = GetComponentInChildren<Animator>();

	        if (animator == null)
	        {
	            Debug.LogWarning("Не туда закинул скрипт!", transform);
	            return;
	        }
        }
        animator.enabled = false;
        StartCoroutine(CheckPlayerDistance());
	}

    private IEnumerator CheckPlayerDistance()
    {
        while (true)
        {
            //if (Vector3.Distance(transform.position, _player.position) <= ActivationDistance)
            float distance = GetDistance();
            if (distance <= ActivationDistance && distance >= 0f)
            {
                animator.enabled = true;
                PlaySound();
                yield break;
            }
            yield return new WaitForSeconds(Delay);
        }
    }

    private float GetDistance()
    {
        if (_animationType == AnimationTypes.Default)
        {
            return Vector3.Distance(transform.position, _player.position);
        }
        else
        {
            return (transform.position.z - _player.position.z);
        }
    }

    private void PlaySound()
    {
        switch (_animationKind)
        {
            case AnimationKinds.Default:
                break;

            case AnimationKinds.PlatformUp:
                _audio.PlaySound(Sounds.PlatformUp);
                break;

            case AnimationKinds.Bridge:
                _audio.PlaySound(Sounds.Bridge);
                break;
        }
    }
}
