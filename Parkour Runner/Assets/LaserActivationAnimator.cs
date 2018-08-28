using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserActivationAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        //var a = new LineRenderer();
        //a.colorGradient.
    }


}
