using UnityEngine;

public class AnimationByTrigger : BaseAnimatorController
{
    [SerializeField] private Animator[] _animators;

    protected override void Init()
    {
        base.Init();

        foreach (var animator in _animators)
            animator.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == _player)
        {
            foreach (var animator in _animators)
                animator.enabled = true;
        }        
    }
}
