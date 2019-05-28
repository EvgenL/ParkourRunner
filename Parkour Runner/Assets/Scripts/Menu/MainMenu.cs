using System.Collections;
using System;
using UnityEngine;
using DG.Tweening;
using AEngine;

public class MainMenu : Menu
{
    [Header("Animation settings")]
    [SerializeField] private GameObject _gameLoader;
    [SerializeField] private SettingsTweening _settingsTweening;    
    [SerializeField] private RectTransform _buttonsShowSpringPoint;
    [SerializeField] private MovingAnimation _buttonsBlockAnim;
    [SerializeField] private MovingAnimation _settingsPanelAnim;
    [SerializeField] private MovingAnimation _playerStatusAnim;
        
    protected override void Show()
    {
        base.Show();
        
        var settingsSecuance = DOTween.Sequence();
        settingsSecuance.Append(_settingsPanelAnim.Show());
                
        var secuance = DOTween.Sequence();
        secuance.Append(_buttonsBlockAnim.Show(_buttonsShowSpringPoint.anchoredPosition, _buttonsBlockAnim.duration * 0.9f));

        secuance.Insert(0, _playerStatusAnim.Show());
                        
        secuance.OnComplete(() =>
        {
            var finalSecuance = DOTween.Sequence();
            finalSecuance.Append(_buttonsBlockAnim.Show(_buttonsBlockAnim.showPoint.anchoredPosition, _buttonsBlockAnim.duration * 0.1f));
        });
    }
        
    protected override void StartHide(Action callback)
    {
        base.StartHide(callback);
        StartCoroutine(HideProcess(callback));
    }

    private IEnumerator HideProcess(Action callback)
    {
        while (_settingsTweening.IsInProcess)
        {
            yield return new WaitForEndOfFrame();
        }
        if (_settingsTweening.IsOpend)
        {
            _settingsTweening.CloseSettings();
        }
        
        var secuance = DOTween.Sequence();
        
        secuance.Append(_buttonsBlockAnim.Hide());
        secuance.Insert(0f, _settingsPanelAnim.Hide());

        secuance.Insert(0, _playerStatusAnim.Hide());
        
        secuance.OnComplete(() =>
        {
            FinishHide(callback);
        });
    }
    
    #region Events
    public void OnPlayButtonClick()
    {
        _audio.PlaySound(Sounds.Tap);
        _menuController.OpenMenu(MenuKinds.SelectLevelType);
    }

    public void OnShopButtonClick()
    {
        _audio.PlaySound(Sounds.Tap);
        _menuController.OpenMenu(MenuKinds.Shop);
    }
    #endregion
}