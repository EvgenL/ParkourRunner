using System;
using UnityEngine;

namespace Basic_Locomotion.Scripts.Generic
{
    [vClassHeader("Frame Limiter", false)]
    public class vFrameLimiter : vMonoBehaviour
    {
        public int desiredFPS = 60;

        void Awake()
        {
            Application.targetFrameRate = -1;
            QualitySettings.vSyncCount = 0;
        }

        void Update()
        {
            long lastTicks = DateTime.Now.Ticks;
            long currentTicks = lastTicks;
            float delay = 1f / desiredFPS;
            float elapsedTime;

            if (desiredFPS <= 0)
                return;

            while (true)
            {
                currentTicks = DateTime.Now.Ticks;
                elapsedTime = (float)TimeSpan.FromTicks(currentTicks - lastTicks).TotalSeconds;
                if (elapsedTime >= delay)
                {
                    break;
                }
            }
        }
    }
}
