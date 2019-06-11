using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialMessage : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _background;
    [SerializeField] private Image _continueButtonImage;
    [SerializeField] private Text _caption;
    [SerializeField] private float _appearDuration;
    [SerializeField] private float _hideDuration;
        
    private void OnEnable()
    {
        TutorialMessageTrigger.OnSendMessage -= OnGetMessage;
        TutorialMessageTrigger.OnSendMessage += OnGetMessage;
                
        _canvasGroup.alpha = 0f;
        _continueButtonImage.raycastTarget = false;
        Time.timeScale = 1f;
    }

    private void OnDisable()
    {
        TutorialMessageTrigger.OnSendMessage -= OnGetMessage;
    }

    private IEnumerator ShowProcess(string text)
    {
        _caption.text = text;

        float time = _appearDuration;

        while (time > 0f)
        {
            _canvasGroup.alpha = Mathf.Clamp01(1f - time / _appearDuration);
            time -= Time.deltaTime;

            yield return null;
        }

        _canvasGroup.alpha = 1f;
        _continueButtonImage.raycastTarget = true;

        yield return new WaitForEndOfFrame();

        Time.timeScale = 0f;
    }

    private IEnumerator HideProcess()
    {
        _continueButtonImage.raycastTarget = false;
        Time.timeScale = 1f;

        float time = _hideDuration;

        while (time > 0f)
        {
            _canvasGroup.alpha = Mathf.Clamp01(time / _appearDuration);
            time -= Time.deltaTime;

            yield return null;
        }

        _canvasGroup.alpha = 0f;
        
        yield return new WaitForEndOfFrame();
    }

    #region Events
    private void OnGetMessage(string message)
    {
        StartCoroutine(ShowProcess(message));
    }

    public void OnContinueClick()
    {
        StartCoroutine(HideProcess());

        //_background.SetActive(false);
        //_continueButtonImage.raycastTarget = false;
        //_caption.enabled = false;
        //Time.timeScale = 1f;
    }
    #endregion
}