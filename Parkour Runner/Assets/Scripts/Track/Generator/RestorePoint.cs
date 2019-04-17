using UnityEngine;
using ParkourRunner.Scripts.Track.Generator;

public class RestorePoint : MonoBehaviour
{
    [SerializeField] private Transform _cachedTransform;

    public Transform CachedTransform { get { return _cachedTransform; } }

    void Start()
    {
        var target = gameObject.GetComponentInParent<Block>();

        if (target != null)
            target.RevivePoints.Add(this);

        if (_cachedTransform == null)
            _cachedTransform = this.transform;
    }
}
