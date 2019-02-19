using UnityEngine;
using System.Collections;
using System;

public class DayTimeGenerator : MonoBehaviour
{
    [Serializable]
    private struct DayTime
    {
        public Color color;
        public float duration;
        public float delay;
    }

    [SerializeField] private Camera _targetCamera;
    [SerializeField] private DayTime[] _dayTimes;

    private Color _currentColor;
    private int _dayIndex;
    
    private void Start()
    {
        if (_dayTimes.Length >= 2)
        {
            print("Start day and night");
            SetColor(_dayTimes[0].color);
            _dayIndex = 0;

            //StartCoroutine(DayTimeProcess());
        }    
        else
        {
            print("Not start");
        }
    }

    private IEnumerator DayTimeProcess()
    {
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
                    SetColor(Color.Lerp(_dayTimes[_dayIndex].color, _dayTimes[nextDayIndex].color, timeRemaining / duration));

                    timeRemaining -= Time.deltaTime;

                    yield return null;
                }

                SetColor(_dayTimes[nextDayIndex].color);

                yield return new WaitForSeconds(_dayTimes[_dayIndex].delay);

                _dayIndex++;
            }

            yield return null;
        }
    }

    private void SetColor(Color color)
    {
        _currentColor = color;
        RenderSettings.fogColor = color;
        _targetCamera.backgroundColor = color;
    }
}
