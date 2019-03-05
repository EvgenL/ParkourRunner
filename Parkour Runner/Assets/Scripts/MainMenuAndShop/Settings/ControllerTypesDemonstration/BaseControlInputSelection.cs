using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using ParkourRunner.Scripts.Player;
using AEngine;
using DG.Tweening;

public abstract class BaseControlInputSelection : MonoBehaviour
{
    private static event Action<bool> OnCheckMode;

    [Serializable]
    protected struct Finger
    {
        public RectTransform transform;
        public Image image;
        public Vector2 pointOffset;
    }

    [Serializable]
    private struct ActiveState
    {
        public Color color;
        public float scale;
    }

    [SerializeField] private ControlsMode _controlMode;
    [SerializeField] private RectTransform _cachedTransform;
    [SerializeField] private Image _background;
    [SerializeField] private ActiveState _enableState;
    [SerializeField] private ActiveState _disableState;
    [SerializeField] private float _statesDuration;
        
    private static bool _lockTouch;

    private void OnEnable()
    {
        OnCheckControlMode(false);
        StartCoroutine(DemonstrationProcess());
        _lockTouch = false;

        OnCheckMode += OnCheckControlMode;
    }

    private void OnDisable()
    {
        StopAllCoroutines();

        OnCheckMode -= OnCheckControlMode;
    }

    protected abstract IEnumerator DemonstrationProcess();

    private IEnumerator ActivateProcess(bool activate)
    {
        _lockTouch = true;

        _cachedTransform.DOScale(activate ? _enableState.scale : _disableState.scale, _statesDuration);
        _background.DOColor(activate ? _enableState.color : _disableState.color, _statesDuration);

        yield return new WaitForSeconds(_statesDuration + 0.1f);

        _lockTouch = false;
    }
    
    #region Events
    public void OnSelectControlMode()
    {
        if (!_lockTouch)
        {
            Configuration.Instance.SaveInputConfiguration(_controlMode);
            AudioManager.Instance.PlaySound(Sounds.Tap);

            OnCheckMode.SafeInvoke(true);
        }
    }

    public void OnCheckControlMode(bool startAnimations)
    {
        bool isActive = _controlMode == Configuration.Instance.GetInputConfiguration();

        if (!startAnimations)
        {
            _cachedTransform.localScale = Vector3.one * (isActive ? _enableState.scale : _disableState.scale);
            _background.color = isActive ? _enableState.color : _disableState.color;
        }
        else
        {
            if (!isActive || (isActive && _background.color == _disableState.color))
            {
                StartCoroutine(ActivateProcess(isActive));
            }
        }
    }
    #endregion
}
