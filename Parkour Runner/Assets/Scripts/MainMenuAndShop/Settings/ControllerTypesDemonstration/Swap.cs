using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Swap : BaseControlInputSelection
{
    [SerializeField] private GameObject _finger;
    [SerializeField] private RectTransform _forFingerPos1;
    [SerializeField] private RectTransform _forFingerPos1End;
    [SerializeField] private RectTransform _forFingerPos2End;
    [SerializeField] private RectTransform _forFingerPos3End;
    [SerializeField] private RectTransform _forFingerPos4End;
    [SerializeField] private float _direction;
        
    private void Start()
    {
        OnCheckControlMode(false);
        _finger.GetComponent<RectTransform>().anchoredPosition = new Vector2(_forFingerPos1.anchoredPosition.x, _forFingerPos1.anchoredPosition.y);
        SwapDemonstrate();
    }

    protected override IEnumerator DemonstrationProcess()
    {
        yield return null;
    }

    private void SwapDemonstrate()
    {
        _finger.GetComponent<RectTransform>().anchoredPosition = new Vector2(_forFingerPos1.anchoredPosition.x, _forFingerPos1.anchoredPosition.y);
        _finger.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        var secuance1 = DOTween.Sequence();
        secuance1.Append(_finger.GetComponent<RectTransform>().DOAnchorPos(_forFingerPos1End.anchoredPosition, _direction));
        secuance1.Insert(0.2f, _finger.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), _direction));
        secuance1.OnComplete(() =>
        {
            _finger.GetComponent<RectTransform>().anchoredPosition = new Vector2(_forFingerPos1.anchoredPosition.x, _forFingerPos1.anchoredPosition.y);
            _finger.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            var secuance2 = DOTween.Sequence();
            secuance2.Append(_finger.GetComponent<RectTransform>().DOAnchorPos(_forFingerPos2End.anchoredPosition, _direction));
            secuance2.Insert(0.2f, _finger.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), _direction));
            secuance2.OnComplete(() =>
            {
                _finger.GetComponent<RectTransform>().anchoredPosition = new Vector2(_forFingerPos1.anchoredPosition.x, _forFingerPos1.anchoredPosition.y);
                _finger.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                var secuance3 = DOTween.Sequence();
                secuance3.Append(_finger.GetComponent<RectTransform>().DOAnchorPos(_forFingerPos3End.anchoredPosition, _direction));
                secuance3.Insert(0.2f, _finger.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), _direction));
                secuance3.OnComplete(() =>
                {
                    _finger.GetComponent<RectTransform>().anchoredPosition = new Vector2(_forFingerPos1.anchoredPosition.x, _forFingerPos1.anchoredPosition.y);
                    _finger.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    var secuance4 = DOTween.Sequence();
                    secuance4.Append(_finger.GetComponent<RectTransform>().DOAnchorPos(_forFingerPos4End.anchoredPosition, _direction));
                    secuance4.Insert(0.2f, _finger.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), _direction));
                    secuance4.OnComplete(() =>
                    {
                        SwapDemonstrate();
                    });

                });
            });
        });
    }
}
