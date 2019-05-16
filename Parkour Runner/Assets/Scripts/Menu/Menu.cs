using System;
using UnityEngine;

public enum MenuKinds
{
    None,
    MainMenu,
    Shop
}

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _menuObject;
    [SerializeField] private MenuKinds _kind;

    private MenuController _menuController;

    public bool IsActive { get { return _menuObject.activeSelf; } set { _menuObject.SetActive(value); } }

    public void Init(MenuController controller)
    {
        if (Application.isPlaying)
        {
            if (_menuObject == null)
                _menuObject = this.gameObject;

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

    protected virtual void Hide(Action callback)
    {
        if (_menuController.CurrentMenu == this)
            _menuController.CurrentMenu = null;

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
            Hide(callback);
    }
    #endregion
}