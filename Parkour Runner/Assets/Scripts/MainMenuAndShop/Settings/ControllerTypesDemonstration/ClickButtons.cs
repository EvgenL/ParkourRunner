using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ClickButtons : BaseControlInputSelection
{
    [Header("Demonstration settings")]
    [SerializeField] private Finger _finger;
    [SerializeField] private float _duration = 0.6f;
    [SerializeField] private float _beforeHideDelay = 0.2f;
    [SerializeField] private RectTransform[] _targets;
    
    protected override IEnumerator DemonstrationProcess()
    {
        int index = 0;

        while (true)
        {
            _finger.transform.anchoredPosition = new Vector2(_targets[index].anchoredPosition.x + _finger.pointOffset.x, _targets[index].anchoredPosition.y + _finger.pointOffset.y);
            _finger.image.color = Color.white;

            yield return new WaitForSeconds(_beforeHideDelay);
                        
            _finger.image.DOColor(new Color(1f, 1f, 1f, 0f), _duration);
            yield return new WaitForSeconds(_duration + 0.1f);

            index = index < _targets.Length - 1 ? index + 1 : 0;
        }
    }
}
