using System;
using UnityEngine;
using DG.Tweening;
using AEngine;

public enum MenuKinds
{
    None,
    MainMenu,
    Shop,
    Characters
}

public class Menu : MonoBehaviour
{
    [Serializable]
    protected struct AnimationSettings
    {
        public RectTransform target;
        public Vector2 showPos;
        public Vector2 hidePos;
        public Ease showEase;
        public Ease hideEase;
        public float duration;
        
        public Tween ShowTween()
        {
            return target.DOAnchorPos(showPos, duration).SetEase(showEase);
        }

        public Tween ShowTween(Vector2 deltaPos, float timeDuration)
        {
            return target.DOAnchorPos(showPos + deltaPos, timeDuration).SetEase(showEase);
        }

        public Tween HideTween()
        {
            return target.DOAnchorPos(hidePos, duration).SetEase(hideEase);
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