using ParkourRunner.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ClickButtons : MonoBehaviour
{
    [SerializeField] private GameObject _finger;
    [SerializeField] private RectTransform _forFingerPos1End;
    [SerializeField] private RectTransform _forFingerPos2End;
    [SerializeField] private RectTransform _forFingerPos3End;
    [SerializeField] private RectTransform _forFingerPos4End;
    [SerializeField] private float _direction;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => Configuration.Instance.SaveInputConfiguration(ControlsMode.FourButtons));
        _finger.GetComponent<RectTransform>().anchoredPosition = new Vector2(_forFingerPos1End.anchoredPosition.x, _forFingerPos1End.anchoredPosition.y);
        ClickDemonstration();
       
    }
    private void ClickDemonstration()
    {
        _finger.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        var secuance1 = DOTween.Sequence();
        _finger.GetComponent<RectTransform>().anchoredPosition = new Vector2(_forFingerPos1End.anchoredPosition.x, _forFingerPos1End.anchoredPosition.y);
        secuance1.Append(_finger.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), _direction));
        secuance1.OnComplete(() =>
        {
            _finger.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            var secuance2 = DOTween.Sequence();
            _finger.GetComponent<RectTransform>().anchoredPosition = new Vector2(_forFingerPos2End.anchoredPosition.x, _forFingerPos2End.anchoredPosition.y);
            secuance2.Append(_finger.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), _direction));
            secuance2.OnComplete(() =>
            {
                _finger.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                var secuance3 = DOTween.Sequence();
                _finger.GetComponent<RectTransform>().anchoredPosition = new Vector2(_forFingerPos3End.anchoredPosition.x, _forFingerPos3End.anchoredPosition.y);
                secuance3.Append(_finger.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), _direction));
                secuance3.OnComplete(() =>
                {
                    _finger.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                    var secuance4 = DOTween.Sequence();
                    _finger.GetComponent<RectTransform>().anchoredPosition = new Vector2(_forFingerPos4End.anchoredPosition.x, _forFingerPos4End.anchoredPosition.y);
                    secuance4.Append(_finger.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), _direction));
                    secuance4.OnComplete(() =>
                    {
                        ClickDemonstration();
                    });
                });
            });

        });

    }
}
