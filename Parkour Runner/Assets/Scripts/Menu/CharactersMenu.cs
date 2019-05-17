using System;
using UnityEngine;
using DG.Tweening;
using AEngine;

public class CharactersMenu : Menu
{
    [Header("Animation settings")]
    [SerializeField] private AnimationSettings _homeButtonAnim;
    [SerializeField] private AnimationSettings _charAnim;

    protected override void Show()
    {
        base.Show();

        if (_charAnim.target != null)
        {
            var secuance = DOTween.Sequence();
            secuance.Append(_charAnim.ShowTween());
            secuance.Insert(0f, _homeButtonAnim.ShowTween());
        }
    }

    protected override void StartHide(Action callback)
    {
        base.StartHide(callback);

        if (_charAnim.target != null)
        {
            var secuance = DOTween.Sequence();

            secuance.Append(_charAnim.HideTween());
            secuance.Insert(0f, _homeButtonAnim.HideTween());

            secuance.OnComplete(() =>
            {
                FinishHide(callback);
            });
        }
    }

    #region Events
    public void OnHomeButtonClick()
    {
        _audio.PlaySound(Sounds.Tap);
        _menuController.OpenMenu(MenuKinds.MainMenu);
    }
    #endregion
}
