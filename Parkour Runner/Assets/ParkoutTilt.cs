	using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;

namespace UnityStandardAssets.CrossPlatformInput
{
    // helps with managing tilt input on mobile devices
    public class ParkoutTilt : MonoBehaviour
    {


        // options for the various orientations
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



        private void Update()
        {
            float angle = 0;
            if (Input.acceleration != Vector3.zero)
            {
                        angle = Mathf.Atan2(Input.acceleration.x, -Input.acceleration.y) * Mathf.Rad2Deg +
                                centreAngleOffset;

            }

            float axisValue = Mathf.InverseLerp(-fullTiltAngle, fullTiltAngle, angle) * 2 - 1;
            switch (mapping.type)
            {
                case AxisMapping.MappingType.NamedAxis:
                    CrossPlatformInputManager.SetAxis(mapping.axisName, axisValue);
                    //m_SteerAxis.Update(axisValue);
                    break;
                case AxisMapping.MappingType.MousePositionX:
                    CrossPlatformInputManager.SetVirtualMousePositionX(axisValue * Screen.width);
                    break;
                case AxisMapping.MappingType.MousePositionY:
                    CrossPlatformInputManager.SetVirtualMousePositionY(axisValue * Screen.width);
                    break;
                case AxisMapping.MappingType.MousePositionZ:
                    CrossPlatformInputManager.SetVirtualMousePositionZ(axisValue * Screen.width);
                    break;
            }
        }
    }
}

