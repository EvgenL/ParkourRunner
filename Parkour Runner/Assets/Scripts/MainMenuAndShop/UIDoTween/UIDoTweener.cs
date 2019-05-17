using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using AEngine;

public class UIDoTweener : MonoBehaviour
{
    private enum AdditionalMenuType
    {
        Shop,
        Characters
    }

    [SerializeField] private GameObject _sceneLoader;

    [SerializeField] private GameObject _playBtn;
    [SerializeField] private RectTransform _playBtnEndPos;
                     private Vector2 _playbtnStartPos;
    [SerializeField] private float _playBtnDuration;

    [SerializeField] private GameObject _shopBtn;
    [SerializeField] private RectTransform _shopBtnEndPos;
    private Vector2 _shopbtnStartPos;
    [SerializeField] private float _shopBtnDuration;

    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private RectTransform _settingsPanelEndPos;
    private Vector2 _settingsPanelStartPos;
    [SerializeField] private float _settingsPanelDuration;

    [SerializeField] private GameObject _shop;
    //[SerializeField] private GameObject _shopContent;
    [SerializeField] private RectTransform _shopEndPos;
    private Vector2 _shopStartPos;
    [SerializeField] private float _shopDuration;

    [SerializeField] private GameObject _playBtnFromShop;
    [SerializeField] private RectTransform _playBtnEndPosFromShop;
    private Vector2 _playbtnStartPosFromShop;
    [SerializeField] private float _playBtnDurationFromShop;

    [SerializeField] private GameObject _backBtnFromShop;
    [SerializeField] private RectTransform _backBtnEndPosFromShop;
    private Vector2 _backbtnStartPosFromShop;
    [SerializeField] private float _backBtnDurationFromShop;

    [SerializeField] private GameObject _avatar;
    [SerializeField] private GameObject _avatarContent;
    [SerializeField] private RectTransform _avatarEndPos;
    private Vector2 _avatarStartPos;
    [SerializeField] private float _avatarDuration;
        
    public static int priority=1;

    private AdditionalMenuType _additionalMenu;


    private void Awake()
    {
        if (_playBtn != null)
            _playbtnStartPos = _playBtn.GetComponent<RectTransform>().anchoredPosition;

        if (_shopBtn != null)
            _shopbtnStartPos = _shopBtn.GetComponent<RectTransform>().anchoredPosition;

        if (_settingsPanel != null)
            _settingsPanelStartPos = _settingsPanel.GetComponent<RectTransform>().anchoredPosition;

        if (_shop != null)
            _shopStartPos = _shop.GetComponent<RectTransform>().anchoredPosition;

        _playbtnStartPosFromShop = _playBtnFromShop.GetComponent<RectTransform>().anchoredPosition;
        _backbtnStartPosFromShop=_backBtnFromShop.GetComponent<RectTransform>().anchoredPosition;
        _avatarStartPos = _avatar.GetComponent<RectTransform>().anchoredPosition;
    }

    private void Start()
    {
        Time.timeScale = 1;
        if (priority == 0)
        {
            AddListeners();
            StartCoroutine(OpenShop());
            return;
        }
        StartCoroutine(EnableBtnsRaycastTargetTo(false, 0));
        var secuance = DOTween.Sequence();

        if (_playBtnEndPos != null)
            secuance.Append(_playBtnEndPos.DOAnchorPos(new Vector2(GetComponent<RectTransform>().rect.width / 2, _playBtnEndPos.anchoredPosition.y), _playBtnDuration).SetEase(Ease.OutCubic));

        if (_shopBtnEndPos != null)
            secuance.Insert(0.1f, _shopBtnEndPos.DOAnchorPos(new Vector2(GetComponent<RectTransform>().rect.width / 2, _shopBtnEndPos.anchoredPosition.y), _shopBtnDuration).SetEase(Ease.OutCubic));

        if (_settingsPanelEndPos != null)
            secuance.Insert(0.3f, _settingsPanelEndPos.DOAnchorPos(new Vector2(_settingsPanelEndPos.anchoredPosition.x, -GetComponent<RectTransform>().rect.height / 2), _settingsPanelDuration).SetEase(Ease.InOutElastic));
        
        secuance.OnComplete(() =>
        {
            StartCoroutine(EnableBtnsRaycastTargetTo(true, 0.1f));
            AddListeners();
        });
    }

    public IEnumerator OpenShop()
    {
        SettingsTweening settings = FindObjectOfType<SettingsTweening>();
        while (settings.IsInProcess)
        {
            yield return new WaitForEndOfFrame();
        }
        if (settings.IsOpend)
        {
            settings.CloseSettings();
        }
        RemoveAllListeners();
        StartCoroutine(EnableBtnsRaycastTargetTo(false, 0));
        var secuance = DOTween.Sequence();

        if (_playBtnEndPos != null)
            secuance.Append(_playBtnEndPos.DOAnchorPos(new Vector2(_playbtnStartPos.x, _playBtnEndPos.anchoredPosition.y), _playBtnDuration * priority).SetEase(Ease.OutCubic));

        if (_shopBtnEndPos != null)
            secuance.Insert(0.1f*priority, _shopBtnEndPos.DOAnchorPos(new Vector2(_shopbtnStartPos.x, _shopBtnEndPos.anchoredPosition.y), _shopBtnDuration * priority).SetEase(Ease.OutCubic));

        if (_settingsPanelEndPos != null)
            secuance.Insert(0.3f * priority, _settingsPanelEndPos.DOAnchorPos(new Vector2(_settingsPanelEndPos.anchoredPosition.x, _settingsPanelStartPos.y), _settingsPanelDuration * priority).SetEase(Ease.InOutElastic));
        
        secuance.Insert(0.5f+_shopDuration, _playBtnEndPosFromShop.DOAnchorPos(new Vector2(-GetComponent<RectTransform>().rect.width / 2, _playBtnEndPosFromShop.anchoredPosition.y), _playBtnDurationFromShop)).SetEase(Ease.InBounce);
        secuance.Insert(0.4f+_settingsPanelDuration, _backBtnEndPosFromShop.DOAnchorPos(new Vector2(-GetComponent<RectTransform>().rect.width / 2, _backBtnEndPosFromShop.anchoredPosition.y), _backBtnDurationFromShop)).SetEase(Ease.InBounce);
         
        if (_shopEndPos != null)
            secuance.Append(_shopEndPos.DOAnchorPos(new Vector2(GetComponent<RectTransform>().rect.width / 2, 0), _shopDuration)).SetEase(Ease.Flash);

        secuance.OnComplete(() => 
        {
            StartCoroutine(EnableBtnsRaycastTargetTo(true, 0.1f));
            priority = 1;
            AddListeners();
            _additionalMenu = AdditionalMenuType.Shop;
        });
    }
    
    private IEnumerator EnableBtnsRaycastTargetTo(bool enable, float yieldRedurnTime)
    {
        yield return new WaitForSeconds(yieldRedurnTime);

        if (_playBtn != null)
            _playBtn.GetComponent<Image>().raycastTarget = enable;

        if (_shopBtn != null)
            _shopBtn.GetComponent<Image>().raycastTarget = enable;

        if (_settingsPanel != null)
            _settingsPanel.transform.GetChild(0).GetComponent<Image>().raycastTarget = enable;

        _backBtnFromShop.GetComponent<Image>().raycastTarget = enable;
        _playBtnFromShop.GetComponent<Image>().raycastTarget = enable;
    }

    private IEnumerator OpenGame()
    {
        SettingsTweening settings = FindObjectOfType<SettingsTweening>();

        yield return new WaitWhile(() => settings.IsInProcess);
        if (settings.IsOpend)
        {
            settings.CloseSettings();
        }
        RemoveAllListeners();
        StartCoroutine(EnableBtnsRaycastTargetTo(false, 0));
                        
        var secuance = DOTween.Sequence();

        if (_playBtnEndPos != null)
            secuance.Append(_playBtnEndPos.DOAnchorPos(new Vector2(_playbtnStartPos.x, _playBtnEndPos.anchoredPosition.y), _playBtnDuration).SetEase(Ease.InOutElastic));

        if (_shopBtnEndPos != null)
            secuance.Insert(0.1f, _shopBtnEndPos.DOAnchorPos(new Vector2(_shopbtnStartPos.x, _shopBtnEndPos.anchoredPosition.y), _shopBtnDuration).SetEase(Ease.InOutElastic));

        if (_settingsPanelEndPos != null)
            secuance.Insert(0.3f, _settingsPanelEndPos.DOAnchorPos(new Vector2(_settingsPanelEndPos.anchoredPosition.x, _settingsPanelStartPos.y), _settingsPanelDuration).SetEase(Ease.InOutElastic));

        secuance.Insert(0.5f , _playBtnEndPosFromShop.DOAnchorPos(new Vector2( _playbtnStartPosFromShop.x, _playBtnEndPosFromShop.anchoredPosition.y), _playBtnDurationFromShop)).SetEase(Ease.InBounce);
        secuance.Insert(0.4f , _backBtnEndPosFromShop.DOAnchorPos(new Vector2(_backbtnStartPosFromShop.x, _backBtnEndPosFromShop.anchoredPosition.y), _backBtnDurationFromShop)).SetEase(Ease.InBounce);

        if (_additionalMenu == AdditionalMenuType.Shop)
        {
            if (_shopEndPos != null)
                secuance.Append(_shopEndPos.DOAnchorPos(new Vector2(_shopStartPos.x, _shopStartPos.y), _shopDuration)).SetEase(Ease.Flash);
        }
        else
            secuance.Append(_avatarEndPos.DOAnchorPos(new Vector2(_avatarStartPos.x, _avatarStartPos.y), _avatarDuration)).SetEase(Ease.Flash);

        secuance.OnComplete(() => { StartCoroutine(EnableBtnsRaycastTargetTo(true, 0.1f)); });
        secuance.OnComplete(() => { OpenGameScene(); } );
    }

    private void OpenGameScene()
    {
        _sceneLoader.SetActive(true);
    }

    private void BackToMain()
    {
        RemoveAllListeners();
        
        StartCoroutine(EnableBtnsRaycastTargetTo(false, 0));
        var firstSecuance = DOTween.Sequence();

        if (_playBtnEndPos != null)
            firstSecuance.Append(_playBtnEndPos.DOAnchorPos(new Vector2(_playbtnStartPos.x, _playBtnEndPos.anchoredPosition.y), _playBtnDuration).SetEase(Ease.InOutElastic));

        if (_shopBtnEndPos != null)
            firstSecuance.Insert(0.1f, _shopBtnEndPos.DOAnchorPos(new Vector2(_shopbtnStartPos.x, _shopBtnEndPos.anchoredPosition.y), _shopBtnDuration).SetEase(Ease.InOutElastic));

        firstSecuance.Insert(0.5f, _playBtnEndPosFromShop.DOAnchorPos(new Vector2(_playbtnStartPosFromShop.x, _playBtnEndPosFromShop.anchoredPosition.y), _playBtnDurationFromShop)).SetEase(Ease.InBounce);
        firstSecuance.Insert(0.4f, _backBtnEndPosFromShop.DOAnchorPos(new Vector2(_backbtnStartPosFromShop.x, _backBtnEndPosFromShop.anchoredPosition.y), _backBtnDurationFromShop)).SetEase(Ease.InBounce);

        if (_additionalMenu == AdditionalMenuType.Shop)
        {
            if (_shopEndPos != null)
                firstSecuance.Append(_shopEndPos.DOAnchorPos(new Vector2(_shopStartPos.x, _shopStartPos.y), _shopDuration)).SetEase(Ease.Flash);
        }
        else
            firstSecuance.Append(_avatarEndPos.DOAnchorPos(new Vector2(_avatarStartPos.x, _avatarStartPos.y), _avatarDuration)).SetEase(Ease.Flash);

        firstSecuance.OnComplete(() => 
        {
            var secuance = DOTween.Sequence();

            if (_playBtnEndPos != null)
                secuance.Append(_playBtnEndPos.DOAnchorPos(new Vector2(GetComponent<RectTransform>().rect.width / 2, _playBtnEndPos.anchoredPosition.y), _playBtnDuration).SetEase(Ease.InOutElastic));

            if (_shopBtnEndPos != null)
                secuance.Insert(0.1f, _shopBtnEndPos.DOAnchorPos(new Vector2(GetComponent<RectTransform>().rect.width / 2, _shopBtnEndPos.anchoredPosition.y), _shopBtnDuration).SetEase(Ease.InOutElastic));

            if (_settingsPanelEndPos != null)
                secuance.Insert(0.3f, _settingsPanelEndPos.DOAnchorPos(new Vector2(_settingsPanelEndPos.anchoredPosition.x, -GetComponent<RectTransform>().rect.height / 2), _settingsPanelDuration).SetEase(Ease.InOutElastic));

            secuance.OnComplete(() => 
            {
                StartCoroutine(EnableBtnsRaycastTargetTo(true, 0.1f));
                AddListeners();
            });
        });
    }

    private IEnumerator OpenCharactersProcess()
    {
        SettingsTweening settings = FindObjectOfType<SettingsTweening>();
        
        yield return new WaitWhile(() => settings.IsInProcess);
        if (settings.IsOpend)
            settings.CloseSettings();
        
        RemoveAllListeners();
        StartCoroutine(EnableBtnsRaycastTargetTo(false, 0));
        var secuance = DOTween.Sequence();

        if (_playBtnEndPos != null)
            secuance.Append(_playBtnEndPos.DOAnchorPos(new Vector2(_playbtnStartPos.x, _playBtnEndPos.anchoredPosition.y), _playBtnDuration * priority).SetEase(Ease.OutCubic));

        if (_shopBtnEndPos != null)
            secuance.Insert(0.1f, _shopBtnEndPos.DOAnchorPos(new Vector2(_shopbtnStartPos.x, _shopBtnEndPos.anchoredPosition.y), _shopBtnDuration * priority).SetEase(Ease.OutCubic));

        if (_settingsPanelEndPos != null)
            secuance.Insert(0.3f, _settingsPanelEndPos.DOAnchorPos(new Vector2(_settingsPanelEndPos.anchoredPosition.x, _settingsPanelStartPos.y), _settingsPanelDuration * priority).SetEase(Ease.InOutElastic));

        secuance.Insert(0.5f + _avatarDuration, _playBtnEndPosFromShop.DOAnchorPos(new Vector2(-GetComponent<RectTransform>().rect.width / 2, _playBtnEndPosFromShop.anchoredPosition.y), _playBtnDurationFromShop)).SetEase(Ease.InBounce);
        secuance.Insert(0.4f + _settingsPanelDuration, _backBtnEndPosFromShop.DOAnchorPos(new Vector2(-GetComponent<RectTransform>().rect.width / 2, _backBtnEndPosFromShop.anchoredPosition.y), _backBtnDurationFromShop)).SetEase(Ease.InBounce);

        secuance.Append(_avatarEndPos.DOAnchorPos(new Vector2(GetComponent<RectTransform>().rect.width / 2, 0), _avatarDuration)).SetEase(Ease.Flash);

        secuance.OnComplete(() =>
        {
            StartCoroutine(EnableBtnsRaycastTargetTo(true, 0.1f));
            priority = 1;
            AddListeners();
            _additionalMenu = AdditionalMenuType.Characters;
        });
    }
            
    private void RemoveAllListeners()
    {
        if (_shopBtn != null)
            _shopBtn.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();

        if (_playBtn != null)
        _playBtn.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();

        _playBtnFromShop.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        _backBtnFromShop.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
    }

    private void AddListeners()
    {
        if (_shopBtn != null)
            _shopBtn.gameObject.GetComponent<Button>().onClick.AddListener(OpenShopClick);
        
        if (_playBtn != null)
            _playBtn.gameObject.GetComponent<Button>().onClick.AddListener(() =>StartCoroutine( OpenGame()));

        _playBtnFromShop.gameObject.GetComponent<Button>().onClick.AddListener(() =>StartCoroutine( OpenGame()));
        _backBtnFromShop.gameObject.GetComponent<Button>().onClick.AddListener(() => BackToMain());
    }

    private void OpenShopClick()
    {
        StartCoroutine(OpenShop());
    }

    public void OpenCharactersWindow()
    {
        StartCoroutine(OpenCharactersProcess());
    }
}