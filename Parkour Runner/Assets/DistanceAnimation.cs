using System.Collections;
using UnityEngine;


public class DistanceAnimation : MonoBehaviour
{
    public float ActivationDistance = 10f;

    private Transform _player;
    private Animator animator;

    private static float Delay = 0.3f;


	void Start ()
	{
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
            if (Vector3.Distance(transform.position, _player.position) <= ActivationDistance)
            {
                animator.enabled = true;
                yield break;
            }
            yield return new WaitForSeconds(Delay);
        }
    }

}
