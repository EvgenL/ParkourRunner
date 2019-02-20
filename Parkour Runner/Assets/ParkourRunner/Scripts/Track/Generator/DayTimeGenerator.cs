using UnityEngine;
using System.Collections;
using System;

public class DayTimeGenerator : MonoBehaviour
{
    [Serializable]
    private struct DayTime
    {
        public Color cameraColor;
        public Color fogColor;        
        public float fogDensity;
        public float duration;
        public float nextDelay;
    }

    [SerializeField] private Camera _targetCamera;
    [SerializeField] private float _startDelay;
    [SerializeField] private DayTime[] _dayTimes;

    private int _dayIndex;
    
    private void Start()
    {
        if (_dayTimes.Length > 0)
        {
            _dayIndex = 0;
            SetDayTime(_dayTimes[_dayIndex]);

            if (_dayTimes.Length > 1)
                StartCoroutine(DayTimeProcess());
        }
    }

    private IEnumerator DayTimeProcess()
    {
        yield return new WaitForSeconds(_startDelay);

        int nextDayIndex = 0;

        while (true)
        {
            if (_dayIndex >= _dayTimes.Length)
                _dayIndex = 0;

            nextDayIndex = (_dayIndex + 1 <= _dayTimes.Length - 1) ? _dayIndex + 1 : 0;

            float duration = _dayTimes[_dayIndex].duration;

            if (duration > 0f)
            {
                float timeRemaining = duration;
                float progress;

                while (timeRemaining > 0f)
                {
                    progress = Mathf.Clamp01(1f - timeRemaining / duration);
                    
                    LerpDayTime(_dayTimes[_dayIndex], _dayTimes[nextDayIndex], progress);
                    
                    timeRemaining -= Time.deltaTime;

                    yield return null;
                }

                SetDayTime(_dayTimes[nextDayIndex]);
                
                yield return new WaitForSeconds(_dayTimes[_dayIndex].nextDelay);

                _dayIndex++;
            }

            yield return null;
        }
    }

    private void LerpDayTime(DayTime from, DayTime to, float progress)
    {
        _targetCamera.backgroundColor = Color.Lerp(from.cameraColor, to.cameraColor, progress);
        RenderSettings.fogColor = Color.Lerp(from.fogColor, to.fogColor, progress);
        RenderSettings.fogDensity = Mathf.Lerp(from.fogDensity, to.fogDensity, progress);
    }

    private void SetDayTime(DayTime dayTime)
    {
        const float MAX_NORMALIZED_VALUE = 1f;
        LerpDayTime(dayTime, dayTime, MAX_NORMALIZED_VALUE);
    }
}
