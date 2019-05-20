using System;
using UnityEngine;
using DG.Tweening;
using AEngine;

public class CharactersMenu : Menu
{
    [Header("Animation settings")]
    [SerializeField] private MovingAnimation _homeButtonAnim;
    [SerializeField] private MovingAnimation _charAnim;

    protected override void Show()
    {
        base.Show();

        if (_charAnim.target != null)
        {
            var secuance = DOTween.Sequence();
            secuance.Append(_charAnim.Show());
            secuance.Insert(0f, _homeButtonAnim.Show());
        }
    }

    protected override void StartHide(Action callback)
    {
        base.StartHide(callback);

        if (_charAnim.target != null)
        {
            var secuance = DOTween.Sequence();

            secuance.Append(_charAnim.Hide());
            secuance.Insert(0f, _homeButtonAnim.Hide());

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
