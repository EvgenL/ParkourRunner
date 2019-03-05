using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Swap : BaseControlInputSelection
{
    [Header("Demonstration settings")]
    [SerializeField] private Finger _finger;
    [SerializeField] private RectTransform _start;
    [SerializeField] private RectTransform[] _targets;
    [SerializeField] private Image[] _targetImages;
    [SerializeField] private Color _enableImgColor;
    [SerializeField] private Color _disableImgColor;
    [SerializeField] private float _duration;
    
    protected override IEnumerator DemonstrationProcess()
    {
        while (true)
        {
            for (int i = 0; i < _targetImages.Length; i++)
                _targetImages[i].color = _disableImgColor;

            for (int i = 0; i < _targets.Length; i++)
            {
                _finger.transform.anchoredPosition = _start.anchoredPosition;
                _finger.image.color = Color.white;

                _finger.transform.DOAnchorPos(_targets[i].anchoredPosition, _duration);
                yield return new WaitForSeconds(0.2f);
                _finger.image.DOColor(new Color(1f, 1f, 1f, 0f), _duration);

                yield return new WaitForSeconds(_duration + 0.1f);
            }

            for (int i = 0; i < _targetImages.Length; i++)
            {
                _finger.transform.anchoredPosition = _targetImages[i].rectTransform.anchoredPosition;
                _finger.image.color = Color.white;

                _targetImages[i].color = _enableImgColor;
                _targetImages[i].DOColor(_disableImgColor, _duration);

                yield return new WaitForSeconds(0.2f);
                _finger.image.DOColor(new Color(1f, 1f, 1f, 0f), _duration);

                yield return new WaitForSeconds(_duration + 0.1f);
            }
        }
    }
}
