using System.Collections;
using UnityEngine;

public class DistanceAnimation : BaseAnimatorController
{
    public enum AnimationTypes
    {
        Default,
        ByZ
    }

    private static float Delay = 0.3f;
        
    [SerializeField] private AnimationTypes _animationType;

    public float ActivationDistance = 10f;
        
    private Animator animator;

    protected override void Init()
    { 
        base.Init();
        
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
}
