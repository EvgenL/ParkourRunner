using System;
using UnityEngine;

public class ShopResolutionController : MonoBehaviour
{
    [Serializable]
    private struct ResulutionBlock
    {
        public float proportion;
        public Vector3 scale;
    }

    [SerializeField] private RectTransform _target;
    [SerializeField] private ResulutionBlock[] _proportionsSettings;

    private void OnEnable()
    {
        float proportion = ((float)Screen.width) / Screen.height;

        print(proportion);

        for (int i = 0; i < _proportionsSettings.Length; i++)
        {
            if (proportion <= _proportionsSettings[i].proportion)
            {
                _target.localScale = _proportionsSettings[i].scale;
                print(_target.localScale);
                return;
            }
        }

        _target.localScale = Vector3.one;
    }
}