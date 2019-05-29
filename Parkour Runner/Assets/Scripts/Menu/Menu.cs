using System;
using UnityEngine;
using DG.Tweening;
using AEngine;

public enum MenuKinds
{
    None,
    MainMenu,
    Shop,
    SelectLevelType,
    SelectLevel
}

public class Menu : MonoBehaviour
{
    [Serializable]
    protected struct MovingAnimation
    {
        public RectTransform target;
        public RectTransform showPoint;        
        public RectTransform hidePoint;
        public Ease showEase;
        public Ease hideEase;
        public float duration;
                
        public Tween Show()
        {
            return target.DOAnchorPos(showPoint.anchoredPosition, duration).SetEase(showEase);
        }
                
        public Tween Show(Vector2 targetPoint, float timeDuration)
        {
            return target.DOAnchorPos(targetPoint, timeDuration).SetEase(showEase);
        }
                
        public Tween Hide()
        {
            return target.DOAnchorPos(hidePoint.anchoredPosition, duration).SetEase(hideEase);
        }

        public Tween Hide(Vector2 targetPoint, float timeDuration)
        {
            return target.DOAnchorPos(targetPoint, timeDuration).SetEase(hideEase);
        }
    }

    [Serializable]
    protected struct AlphaAnimation
    {
        public CanvasGroup target;
        public Ease showEase;
        public Ease hideEase;
        public float duration;

        public Tween Show()
        {
            return target.DOFade(1f, duration).SetEase(showEase);
        }

        public Tween Hide()
        {
            return target.DOFade(0f, duration).SetEase(hideEase);
        }
    }

    [SerializeField] private GameObject _menuObject;
    [SerializeField] private MenuKinds _kind;

    protected MenuController _menuController;
    protected AudioManager _audio;

    public MenuKinds Kind { get { return _kind; } }
    public bool IsActive { get { return _menuObject.activeSelf; } set { _menuObject.SetActive(value); } }

    public void Init(MenuController controller)
    {
        if (Application.isPlaying)
        {
            if (_menuObject == null)
                _menuObject = this.gameObject;

            _audio = AudioManager.Instance;
            _menuController = controller;
            this.IsActive = false;

            controller.OnShowMenu += OnShowMenu;
            controller.OnHideMenu += OnHideMenu;
        }
    }

    protected virtual void Show()
    {
        _menuController.CurrentMenu = this;
        this.IsActive = true;
    }

    protected virtual void StartHide(Action callback)
    {
        if (_menuController.CurrentMenu == this)
            _menuController.CurrentMenu = null;
    }

    protected virtual void FinishHide(Action callback)
    {
        callback.SafeInvoke();
        this.IsActive = false;
    }

    #region Events
    private void OnShowMenu(MenuKinds kind)
    {
        if (!this.IsActive && _kind == kind)
            Show();
    }

    private void OnHideMenu(MenuKinds kind, Action callback)
    {
        if (this.IsActive && _kind == kind)
            StartHide(callback);
    }
    #endregion
}