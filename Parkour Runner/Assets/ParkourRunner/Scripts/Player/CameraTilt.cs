using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace ParkourRunner.Scripts.Player
{
    public class CameraTilt : MonoBehaviour
    {
        private Quaternion _baseRotation;
        private Transform _camera;

        private float _maxAngle;

        private void OnEnable()
        {
            _camera = Camera.main.transform;
            _baseRotation = _camera.rotation;
            _maxAngle = GetComponent<ParkoutTilt>().fullTiltAngle;
        }

        private void OnDisable()
        {
            _camera.rotation = _baseRotation;
        }

        void Update ()
        {
            var angleZ = -CrossPlatformInputManager.GetAxis("Horizontal") * _maxAngle;
            _camera.rotation = _baseRotation * Quaternion.Euler(0, 0, angleZ);
        }
    }
}
