using UnityEngine;

[CreateAssetMenu(fileName = "BonusData", menuName = "Bonus Data", order = 52)]
public class BonusData : ScriptableObject
{
    [SerializeField] private BonusName _bonusKind;
    [SerializeField] private float _baseDuration;
    [SerializeField] private float[] _durationPowers;

    public BonusName BonusKind { get { return _bonusKind; } }

    public float BaseDuration { get { return _baseDuration; } }

    public float[] DurationPowers { get { return _durationPowers; } }
}
