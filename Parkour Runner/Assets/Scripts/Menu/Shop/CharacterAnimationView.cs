using System.Collections;
using UnityEngine;

public class CharacterAnimationView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _appearClip;
    [SerializeField] private float _duration;

    private void OnEnable()
    {
        ShopMenu.OnShowMenu -= OnShowMenuFinished;
        ShopMenu.OnShowMenu += OnShowMenuFinished;

        ShopMenu.OnHideMenu -= OnHideMenuFinished;
        ShopMenu.OnHideMenu += OnHideMenuFinished;

        _animator.Play(_appearClip, 0, 0f);
    }

    private void OnDisable()
    {
        ShopMenu.OnShowMenu -= OnShowMenuFinished;
        ShopMenu.OnHideMenu -= OnHideMenuFinished;
    }

    private IEnumerator AnimationProcess()
    {
        float time = _duration;
        
        while (time > 0f)
        {
            _animator.Play(_appearClip, 0, 1f - Mathf.Clamp01(time / _duration));
            time -= Time.deltaTime;

            yield return null;
        }

        _animator.Play(_appearClip, 0, 1f);
    }

    #region Events
    private void OnShowMenuFinished()
    {
        StopAllCoroutines();
        StartCoroutine(AnimationProcess());
    }

    private void OnHideMenuFinished()
    {
        _animator.Play(_appearClip, 0, 0f);
    }
    #endregion
}
