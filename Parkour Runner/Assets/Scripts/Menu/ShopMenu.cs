using System;
using UnityEngine;
using DG.Tweening;
using AEngine;

public class ShopMenu : Menu
{
    [Header("Animation settings")]
    [SerializeField] private MovingAnimation _homeButtonAnim;
    [SerializeField] private MovingAnimation _playerStatusAnim;
    [SerializeField] private MovingAnimation _shopAnim;

    protected override void Show()
    {
        base.Show();
        
        var secuance = DOTween.Sequence();
        secuance.Append(_shopAnim.Show());
        secuance.Insert(0f, _playerStatusAnim.Show());
        secuance.Insert(0f, _homeButtonAnim.Show());
    }

    protected override void StartHide(Action callback)
    {
        base.StartHide(callback);

        var secuance = DOTween.Sequence();

        secuance.Append(_shopAnim.Hide());
        secuance.Insert(0f, _playerStatusAnim.Hide());
        secuance.Insert(0f, _homeButtonAnim.Hide());

        secuance.OnComplete(() =>
        {
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