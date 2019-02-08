using ParkourRunner.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwapTril : MonoBehaviour
{
    [SerializeField] private GameObject _finger;
    [SerializeField] private RectTransform _forFingerPos1;
    [SerializeField] private RectTransform _forFingerPos1End;
    [SerializeField] private RectTransform _forFingerPos2End;
    [SerializeField] private float _direction;
    
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => Configuration.Instance.SaveInputConfiguration(ControlsMode.Tilt));
        _finger.GetComponent<RectTransform>().anchoredPosition = new Vector2(_forFingerPos1.anchoredPosition.x, _forFingerPos1.anchoredPosition.y);
        SwapDemonstrate();
    }

    public void SwapDemonstrate()
    {
        _finger.GetComponent<RectTransform>().anchoredPosition = new Vector2(_forFingerPos1.anchoredPosition.x, _forFingerPos1.anchoredPosition.y);
        _finger.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        var secuance1 = DOTween.Sequence();
        secuance1.Append(_finger.GetComponent<RectTransform>().DOAnchorPos(_forFingerPos1End.anchoredPosition, _direction));
        secuance1.Insert(0.2f, _finger.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), _direction));

        secuance1.OnComplete(() =>
        {
            _finger.GetComponent<RectTransform>().anchoredPosition = new Vector2(_forFingerPos1.anchoredPosition.x, _forFingerPos1.anchoredPosition.y);
            _finger.GetComponent<Image>().color = new Color(1,1,1,1);
            var secuance2 = DOTween.Sequence();
            secuance2.Append(_finger.GetComponent<RectTransform>().DOAnchorPos(_forFingerPos2End.anchoredPosition, _direction));
            secuance2.Insert(0.2f, _finger.GetComponent<Image>().DOColor(new Color(1, 1, 1, 0), _direction));
            secuance2.OnComplete(() =>
            {
                GetComponent<Animation>().Play();
              
                
            });
        });
    }

}
