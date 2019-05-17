using System.Collections;
using System;
using UnityEngine;
using DG.Tweening;
using AEngine;

public class MainMenu : Menu
{
    [Header("Animation settings")]
    [SerializeField] private GameObject _gameLoader;
    [SerializeField] private SettingsTweening _settingsPanel;
    [SerializeField] private Vector2 _buttonsSpringDelta;
    [SerializeField] private AnimationSettings _playButtonAnim;
    [SerializeField] private AnimationSettings _shopButtonAnim;
    [SerializeField] private AnimationSettings _settingsPanelAnim;
    
    protected override void Show()
    {
        base.Show();

        if (_playButtonAnim.target != null && _shopButtonAnim.target != null && _settingsPanelAnim.target != null)
        {
            var settingsSecuance = DOTween.Sequence();
            settingsSecuance.Append(_settingsPanelAnim.ShowTween());

            var secuance = DOTween.Sequence();
            secuance.Append(_playButtonAnim.ShowTween(_buttonsSpringDelta, _playButtonAnim.duration * 0.95f));
            secuance.Insert(0f, _shopButtonAnim.ShowTween(_buttonsSpringDelta, _shopButtonAnim.duration * 0.95f));
            
            secuance.OnComplete(() =>
            {
                var finalSecuance = DOTween.Sequence();

                finalSecuance.Append(_playButtonAnim.ShowTween(Vector2.zero, _playButtonAnim.duration * 0.05f));
                finalSecuance.Insert(0f, _shopButtonAnim.ShowTween(Vector2.zero, _shopButtonAnim.duration * 0.05f));
            });
        }
    }

    protected override void StartHide(Action callback)
    {
        base.StartHide(callback);
        StartCoroutine(HideProcess(callback));
    }

    private IEnumerator HideProcess(Action callback)
    {
        while (_settingsPanel.IsInProcess)
        {
            yield return new WaitForEndOfFrame();
        }
        if (_settingsPanel.IsOpend)
        {
            _settingsPanel.CloseSettings();
        }
        
        if (_playButtonAnim.target != null && _shopButtonAnim.target != null && _settingsPanelAnim.target != null)
        {
            var secuance = DOTween.Sequence();

            secuance.Append(_playButtonAnim.HideTween());
            secuance.Insert(0.1f, _shopButtonAnim.HideTween());
            secuance.Insert(0.1f, _settingsPanelAnim.HideTween());

            secuance.OnComplete(() =>
            {
                FinishHide(callback);
            });
        }
    }

    private void OpenGame()
    {
        _gameLoader.SetActive(true);
    }

    #region Events
    public void OnPlayButtonClick()
    {
        _audio.PlaySound(Sounds.Tap);
        StartHide(OpenGame);
    }

    public void OnShopButtonClick()
    {
        _audio.PlaySound(Sounds.Tap);
        _menuController.OpenMenu(MenuKinds.Shop);
    }
    #endregion
}