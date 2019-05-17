using System;
using UnityEngine;
using DG.Tweening;
using AEngine;

public class ShopMenu : Menu
{
    [Header("Animation settings")]
    [SerializeField] private AnimationSettings _homeButtonAnim;
    [SerializeField] private AnimationSettings _shopAnim;

    protected override void Show()
    {
        base.Show();
        
        var secuance = DOTween.Sequence();
        secuance.Append(_shopAnim.ShowTween());
        secuance.Insert(0f, _homeButtonAnim.ShowTween());
    }

    protected override void StartHide(Action callback)
    {
        base.StartHide(callback);

        var secuance = DOTween.Sequence();

        secuance.Append(_shopAnim.HideTween());
        secuance.Insert(0f, _homeButtonAnim.HideTween());

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