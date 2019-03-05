using UnityEngine;
using DG.Tweening;
using System.Collections;

public class SwapTril : BaseControlInputSelection
{
    [Header("Demonstration settings")]
    [SerializeField] private Finger _finger;
    [SerializeField] private RectTransform _startPosition;
    [SerializeField] private RectTransform[] _targets;
    [SerializeField] private float _duration;
    
    public void SwapDemonstrate()
    {
        StartCoroutine(DemonstrationProcess());
    }

    protected override IEnumerator DemonstrationProcess()
    {
        for (int i = 0; i < _targets.Length; i++)
        {
            _finger.transform.anchoredPosition = _startPosition.anchoredPosition;
            _finger.image.color = Color.white;

            _finger.transform.DOAnchorPos(_targets[i].anchoredPosition, _duration);
            yield return new WaitForSeconds(0.2f);
            _finger.image.DOColor(new Color(1f, 1f, 1f, 0f), _duration);

            yield return new WaitForSeconds(_duration + 0.1f);
        }

        this.gameObject.GetComponent<Animation>().Play();
    }
}
