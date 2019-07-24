using UnityEngine;
using UnityEngine.Analytics;
using ParkourRunner.Scripts.Managers;

public class DistanceAnalitycsEvent : MonoBehaviour
{
    [SerializeField] private AnalyticsEventTracker _analitycsEvent;

    public float Distance { get; set; }

    private void OnEnable()
    {
        this.Distance = GameManager.Instance.DistanceRun;
        _analitycsEvent.TriggerEvent();
    }
}