using System;
using UnityEngine;
using DG.Tweening;
using AEngine;

public class SelectLevelMenu : Menu
{
    [SerializeField] private GameObject _gameLoader;

    [Header("Animation settings")]
    [SerializeField] private MovingAnimation _backButtonAnim;
    [SerializeField] private AlphaAnimation _levelsAnim;

    protected override void Show()
    {
        base.Show();

        var secuance = DOTween.Sequence();
        secuance.Append(_levelsAnim.Show());
        secuance.Insert(0.1f, _backButtonAnim.Show());
    }

    protected override void StartHide(Action callback)
    {
        base.StartHide(callback);

        var secuance = DOTween.Sequence();
        secuance.Append(_backButtonAnim.Hide());
        secuance.Insert(0.2f, _levelsAnim.Hide());

        secuance.OnComplete(() =>
        {
            FinishHide(callback);
        });
    }

    private void OpenGame()
    {
        MenuController.TransitionTarget = MenuKinds.None;
        _gameLoader.SetActive(true);
    }

    #region Events
    public void OnBackButtonClick()
    {
        _audio.PlaySound(Sounds.Tap);
        _menuController.OpenMenu(MenuKinds.SelectLevelType);
    }

    public void OnLevelSelectClick(LevelItem item)
    {
        if (item.IsActive)
        {
            _audio.PlaySound(Sounds.Tap);

            EnvironmentController.CheckKeys();
            PlayerPrefs.SetInt(EnvironmentController.TUTORIAL_KEY, 0);
            PlayerPrefs.SetInt(EnvironmentController.ENDLESS_KEY, 0);
            PlayerPrefs.SetInt(EnvironmentController.LEVEL_KEY, item.Level);
            PlayerPrefs.Save();

            StartHide(OpenGame);
        }
    }
    #endregion
}