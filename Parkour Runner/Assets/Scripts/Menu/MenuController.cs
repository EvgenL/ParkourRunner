using System;
using UnityEngine;
using AEngine;

public class MenuController : MonoBehaviour
{
    public static MenuKinds TransitionTarget { get; set; }

    public event Action<MenuKinds> OnShowMenu;
    public event Action<MenuKinds, Action> OnHideMenu;

    [SerializeField] private MenuKinds _defaultMenu;
    [SerializeField] private Menu[] menus;

    private AudioManager _audio;

    public MenuKinds TargetMenu { get; private set; }
    public Menu CurrentMenu { get; set; }
    
    private void Awake()
    {
        _audio = AudioManager.Instance;
        _audio.LoadAudioBlock(AudioBlocks.Menu);
        _audio.PlayMusic();

        this.CurrentMenu = null;

        foreach (Menu item in menus)
            item.Init(this);

        Time.timeScale = 1f;

        OpenMenu(TransitionTarget == MenuKinds.None ? MenuKinds.MainMenu : TransitionTarget);
    }
        
    public void OpenMenu(MenuKinds menu)
    {
        this.TargetMenu = menu;

        if (this.CurrentMenu != null)
            OnHideMenu(this.CurrentMenu.Kind, ShowMenu);
        else
            ShowMenu();
    }

    private void ShowMenu()
    {
        this.OnShowMenu.SafeInvoke(TargetMenu);
    }
}