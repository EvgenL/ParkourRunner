using System;
using UnityEngine;

public class ViewByResolution : MonoBehaviour
{
    [Serializable]
    private struct ResulutionBlock
    {
        public float proportion;
        public Vector3 scale;
    }

    [Serializable]
    private struct MovingBlock
    {
        public float proportion;
        public Vector2 position;
    }

    [SerializeField] private RectTransform _targetTransform;
    [SerializeField] private ResulutionBlock[] _scales;
    [SerializeField] private MovingBlock[] _positions;

    private void OnEnable()
    {
        float proportion = ((float)Screen.width) / Screen.height;
        bool wasFlag = false;

        for (int i = 0; i < _scales.Length; i++)
        {
            if (proportion <= _scales[i].proportion)
            {
                _targetTransform.localScale = _scales[i].scale;
                wasFlag = true;
                break;
            }
        }

        if (!wasFlag)
            _targetTransform.localScale = Vector3.one;
                
        for (int i = 0; i < _positions.Length; i++)
        {
            if (proportion <= _positions[i].proportion)
            {
                _targetTransform.anchoredPosition = _positions[i].position;
                return;
            }
        }
    }
}