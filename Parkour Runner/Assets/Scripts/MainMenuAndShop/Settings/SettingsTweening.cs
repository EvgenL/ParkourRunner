using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using AEngine;

public class SettingsTweening : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float _duration;
    [SerializeField] private float _feelAmountDuration;

    [SerializeField] private Sprite[] _baseBtnImgs;

    [SerializeField] private GameObject _baseBtn;
    [SerializeField] private GameObject _AGBtn;
    [SerializeField] private GameObject _likeBtn;
    [SerializeField] private GameObject _soundBtn;
    [SerializeField] private GameObject _musicBtn;    
    [SerializeField] private Ease _ease;

    [Header("Control background")]
    [SerializeField] private GameObject _controll;
    [SerializeField] private Image _controlBackground;
    [SerializeField] private RectTransform _showAnchor;
    [SerializeField] private RectTransform _hideAnchor;
    [SerializeField] private Color _enableColorBg;
    [SerializeField] private Color _disableColorBg;
    [SerializeField] private float _controlBackgroundDuration;

    public bool IsOpend { get; private set; }
    public bool IsInProcess { get; private set; }

    private void Start()
    {
        _baseBtn.GetComponent<Button>().onClick.AddListener(() => OpenSettings());
    }
    
    private void OpenSettings()
    {
        _baseBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        RemoveListenersOfSettings();
        OpenNext(_AGBtn, _baseBtn);

        AudioManager.Instance.PlaySound(Sounds.Tap);
        AudioManager.Instance.PlaySound(Sounds.WinSimple);
    }

    public void CloseSettings()
    {
        _baseBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        RemoveListenersOfSettings();
        ClosePrevious(_musicBtn, _soundBtn);
        
        AudioManager.Instance.PlaySound(Sounds.Tap);
        AudioManager.Instance.PlaySound(Sounds.WinSimple);
    }

    private void OpenNext(GameObject current,GameObject baseObj)
    {
        IsInProcess = true;
        current.SetActive(true);
        current.transform.SetParent( baseObj.transform.parent);

        var secuance = DOTween.Sequence();
        RectTransform baseRect = baseObj.GetComponent<RectTransform>();
        secuance.Append(current.GetComponent<RectTransform>().DOAnchorPos(new Vector2(baseRect.anchoredPosition.x - baseRect.rect.width - distance, baseRect.anchoredPosition.y), _duration).SetEase(_ease));
        secuance.Insert(0.1f * _duration, current.GetComponent<Image>().DOFillAmount(1, _feelAmountDuration));
        
        secuance.OnComplete(() =>
        {
            if (current.transform.childCount == 0)
            {
                var controllsecuance = DOTween.Sequence();
                controllsecuance.Append(_controll.GetComponent<RectTransform>().DOAnchorPos(_showAnchor.anchoredPosition, 0.2f));
                _controlBackground.DOColor(_enableColorBg, 0.5f);

                controllsecuance.OnComplete(() =>
                {
                    _baseBtn.GetComponent<Image>().sprite = _baseBtnImgs[1];
                    _baseBtn.GetComponent<Button>().onClick.AddListener(() => CloseSettings());
                    AddListenersForSettings();
                    IsOpend = true;
                    IsInProcess = false;
                    return;
                });
            }

            OpenNext(current.transform.GetChild(0).gameObject, current);
        });
    }

    private void ClosePrevious(GameObject current , GameObject baseObj)
    {
        IsInProcess = true;
         current.transform.SetParent ( baseObj.transform);

        var secuance = DOTween.Sequence();
        secuance.Append(current.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), _duration).SetEase(_ease));
        secuance.Insert(0.1f * _duration,current.GetComponent<Image>().DOFillAmount(0, _feelAmountDuration));

        secuance.OnComplete(() =>
        {
            current.SetActive(false);
            if (baseObj == _baseBtn)
            {
                var controllsecuance = DOTween.Sequence();
                controllsecuance.Append(_controll.GetComponent<RectTransform>().DOAnchorPos(_hideAnchor.anchoredPosition, 0.2f));
                _controlBackground.DOColor(_disableColorBg, 0.5f);

                controllsecuance.OnComplete(() =>
                {
                    _baseBtn.GetComponent<Image>().sprite = _baseBtnImgs[0];
                    _baseBtn.GetComponent<Button>().onClick.AddListener(() => OpenSettings());
                    IsOpend = false;
                    IsInProcess = false;
                    return;
                });
            }
            ClosePrevious(baseObj, _baseBtn.transform.parent.transform.GetChild(baseObj.transform.GetSiblingIndex()-1).gameObject);
        });
    }

    private void AddListenersForSettings()
    {
        _AGBtn.GetComponent<Button>().onClick.AddListener(() => _AGBtn.GetComponent<SettingsBase>().OnClick());
        _likeBtn.GetComponent<Button>().onClick.AddListener(() => _likeBtn.GetComponent<SettingsBase>().OnClick());
        _soundBtn.GetComponent<Button>().onClick.AddListener(() => _soundBtn.GetComponent<SettingsBase>().OnClick());
        _musicBtn.GetComponent<Button>().onClick.AddListener(() =>_musicBtn.GetComponent<SettingsBase>().OnClick());
    }

    private void RemoveListenersOfSettings()
    {
        _AGBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        _likeBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        _soundBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        _musicBtn.GetComponent<Button>().onClick.RemoveAllListeners();
    }
}