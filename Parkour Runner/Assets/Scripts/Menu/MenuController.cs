using System;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuKinds TransitionTarget { get; set; }

    public event Action<MenuKinds> OnShowMenu;
    public event Action<MenuKinds, Action> OnHideMenu;

    [SerializeField] private MenuKinds _defaultMenu;
    [SerializeField] private Menu[] menus;

    public Menu CurrentMenu { get; set; }

    private void Awake()
    {
        foreach (Menu item in menus)
            item.Init(this);

        OnShowMenu.SafeInvoke(TransitionTarget == MenuKinds.None ? _defaultMenu : TransitionTarget);
    }
}