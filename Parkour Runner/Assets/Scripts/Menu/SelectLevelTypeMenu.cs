using System;
using UnityEngine;
using DG.Tweening;
using AEngine;

public class SelectLevelTypeMenu : Menu
{
    [Header("Animation settings")]
    [SerializeField] private AnimationSettings _backButtonAnim;

    protected override void Show()
    {
        base.Show();
                
        var secuance = DOTween.Sequence();
        secuance.Append(_backButtonAnim.ShowTween());
    }

    protected override void StartHide(Action callback)
    {
        base.StartHide(callback);

        var secuance = DOTween.Sequence();
        secuance.Append(_backButtonAnim.HideTween());
        
        secuance.OnComplete(() =>
        {
            FinishHide(callback);
        });
    }

    #region Events
    public void OnBackButtonClick()
    {
        _audio.PlaySound(Sounds.Tap);
        _menuController.OpenMenu(MenuKinds.MainMenu);
    }

    public void OnEnglessLevelClick()
    {
        _audio.PlaySound(Sounds.Tap);
    }

    public void OnSelectLevelClick()
    {
        _audio.PlaySound(Sounds.Tap);
    }
    #endregion
}
