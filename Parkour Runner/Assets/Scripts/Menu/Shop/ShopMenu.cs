using System;
using UnityEngine;
using DG.Tweening;
using AEngine;

public class ShopMenu : Menu
{
    public static event Action OnShowMenu;
    public static event Action OnHideMenu;

    [Header("Animation settings")]
    [SerializeField] private float _showAfterBgDelay;
    [SerializeField] private float _hideForBgDelay;
    [SerializeField] private MovingAnimation _homeButtonAnim;
    [SerializeField] private MovingAnimation _playerStatusAnim;
    [SerializeField] private MovingAnimation _shopAnim;
    [SerializeField] private AlphaAnimation _backgroundAnim;    

    protected override void Show()
    {
        base.Show();
        
        var secuance = DOTween.Sequence();
        secuance.Append(_backgroundAnim.Hide());

        secuance.Insert(_showAfterBgDelay, _shopAnim.Show());
        secuance.Insert(_showAfterBgDelay, _playerStatusAnim.Show());
        secuance.Insert(_showAfterBgDelay, _homeButtonAnim.Show());

        secuance.OnComplete(() =>
        {
            OnShowMenu.SafeInvoke();
        });
    }

    protected override void StartHide(Action callback)
    {
        base.StartHide(callback);

        var secuance = DOTween.Sequence();

        secuance.Append(_shopAnim.Hide());
        
        secuance.Insert(0f, _playerStatusAnim.Hide());
        secuance.Insert(0f, _homeButtonAnim.Hide());
        secuance.Insert(_hideForBgDelay, _backgroundAnim.Show());
        
        secuance.OnComplete(() =>
        {
            OnHideMenu.SafeInvoke();
            FinishHide(callback);
        });
    }

    #region Events
    public void OnHomeButtonClick()
    {
        _audio.PlaySound(Sounds.Tap);
        _menuController.OpenMenu(MenuKinds.MainMenu);
    }
    #endregion
}