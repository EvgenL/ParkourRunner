using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CameraTilt : MonoBehaviour
{
    private Quaternion _baseRotation;
    private Transform _camera;

    private float _maxAngle;

    private void Start()
    {
        _camera = Camera.main.transform;
        _baseRotation = _camera.rotation;
        _maxAngle = GetComponent<TiltInput>().fullTiltAngle;
    }

	void Update ()
	{
	    var angleZ = -CrossPlatformInputManager.GetAxis("Horizontal") * _maxAngle;
        _camera.rotation = _baseRotation * Quaternion.Euler(0, 0, angleZ);
	}
}
