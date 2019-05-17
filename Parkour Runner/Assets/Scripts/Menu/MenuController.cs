using System;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuKinds TransitionTarget { get; set; }

    public event Action<MenuKinds> OnShowMenu;
    public event Action<MenuKinds, Action> OnHideMenu;

    [SerializeField] private MenuKinds _defaultMenu;
    [SerializeField] private Menu[] menus;

    private MenuKinds _targetMenu;

    public Menu CurrentMenu { get; set; }

    private void Awake()
    {
        foreach (Menu item in menus)
            item.Init(this);

        OnShowMenu.SafeInvoke(TransitionTarget == MenuKinds.None ? _defaultMenu : TransitionTarget);
    }

    public void OpenMenu(MenuKinds menu)
    {
        _targetMenu = menu;

        if (this.CurrentMenu != null)
            OnHideMenu(this.CurrentMenu.Kind, ShowMenu);
        else
            ShowMenu();
    }

    private void ShowMenu()
    {
        OnShowMenu.SafeInvoke(_targetMenu);
    }
}