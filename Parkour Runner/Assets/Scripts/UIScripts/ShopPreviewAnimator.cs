using System.Collections;
using UnityEngine;

namespace ParkourRunner.Scripts.UIScripts
{
    public class ShopPreviewAnimator : MonoBehaviour
    {
        private Animator _animator;

        public string AnimationName = "JumpOver";

        void Start ()
        {
            _animator = GetComponent<Animator>();
            _animator.Play(AnimationName);
            StartCoroutine(PlayAnimation());
        }

        private  IEnumerator PlayAnimation()
        {
            while (true)
            {
                if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(AnimationName) && !_animator.IsInTransition(0))
                {
                    yield return new WaitForSeconds(2f);
                    _animator.CrossFadeInFixedTime(AnimationName, 0.25f);
                }

            }
        }
    }
}
