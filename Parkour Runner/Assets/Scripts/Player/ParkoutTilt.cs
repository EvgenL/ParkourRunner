using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
#if UNITY_EDITOR
#endif

namespace ParkourRunner.Scripts.Player
{
    public class ParkoutTilt : MonoBehaviour
    {
        public enum AxisOptions
        {
            ForwardAxis,
            SidewaysAxis,
        }

        [Serializable]
        public class AxisMapping
        {
            public enum MappingType
            {
                NamedAxis,
                MousePositionX,
                MousePositionY,
                MousePositionZ
            };


            public MappingType type;
            public string axisName;
        }

        public AxisMapping mapping;
        public float fullTiltAngle = 25;
        public float centreAngleOffset = 0;

        public int BufferSize = 7; //Между сколькими кадрами высчитывается сглаженное значение

        private float[] _buffer;
        private float _smoothValue;

        private void Start()
        {
            _buffer = new float[BufferSize];
        }

        private void Update()
        {
            float angle = 0;
            if (Input.acceleration != Vector3.zero)
            {
                angle = Mathf.Atan2(Input.acceleration.x, -Input.acceleration.y) * Mathf.Rad2Deg +
                        centreAngleOffset;
            }

            float mappedValue = Mathf.InverseLerp(-fullTiltAngle, fullTiltAngle, angle) * 2 - 1;
            if (BufferSize <= 1)
            {
                _smoothValue = mappedValue;
                return;
            }

            //write to buffer
            for (int i = 1; i < BufferSize; i++)
            {
                _buffer[i - 1] = _buffer[i];
            }

            _buffer[BufferSize - 1] = mappedValue;

            //get _smoothValue
            _smoothValue = 0;
            for (int i = 0; i < BufferSize; i++)
            {
                _smoothValue += _buffer[i];
            }
            _smoothValue /= _buffer.Length;




            switch (mapping.type)
            {
                case AxisMapping.MappingType.NamedAxis:
                    CrossPlatformInputManager.SetAxis(mapping.axisName, _smoothValue);
                    //m_SteerAxis.Update(axisValue);
                    break;
                case AxisMapping.MappingType.MousePositionX:
                    CrossPlatformInputManager.SetVirtualMousePositionX(mappedValue * Screen.width);
                    break;
                case AxisMapping.MappingType.MousePositionY:
                    CrossPlatformInputManager.SetVirtualMousePositionY(mappedValue * Screen.width);
                    break;
                case AxisMapping.MappingType.MousePositionZ:
                    CrossPlatformInputManager.SetVirtualMousePositionZ(mappedValue * Screen.width);
                    break;
            }
        }
    }
}

