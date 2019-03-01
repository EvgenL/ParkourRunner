using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace ParkourRunner.Scripts.Player
{
    public class CameraTilt : MonoBehaviour
    {
        private Quaternion _baseRotation;
        private ParkourCamera _camera;

        private float _maxAngle;

        private void OnEnable()
        {
            _camera = FindObjectOfType<ParkourCamera>();
            _baseRotation = _camera.transform.rotation;
            _maxAngle = GetComponent<ParkoutTilt>().fullTiltAngle;
        }

        private void OnDisable()
        {
            _camera.YAngle = 0f; //rotation = _baseRotation;
        }

        void Update ()
        {
            var angleZ = -CrossPlatformInputManager.GetAxis("Horizontal") * _maxAngle;
            _camera.YAngle = angleZ; //rotation = _baseRotation * Quaternion.Euler(0, 0, angleZ);
        }
    }
}
