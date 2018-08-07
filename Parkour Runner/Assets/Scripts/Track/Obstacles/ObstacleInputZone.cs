using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.InvectorMods;
using Invector.CharacterController;
using UnityEngine;

public class ObstacleInputZone : MonoBehaviour {
    
    public List<vTriggerGenericAction> Triggers = new List<vTriggerGenericAction>();
    //public List<vObjectDamage> Killers = new List<vObjectDamage>();

    private ParkourThirdPersonInput _input;

    private void Start()
    {
        foreach (var trig in Triggers)
        {
            trig.OnDoAction.AddListener(OnTriggerUsed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        ParkourThirdPersonInput c = other.GetComponent<ParkourThirdPersonInput>();
        if (c == null) return;
        _input = c;
        _input.EnterInputZone(this);
    }

    private void OnTriggerStay(Collider other)
    {
        if (_input)
        _input.EnterInputZone(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_input)
        _input.ExitInputZone();
        ReadyToJump(false);
    }

    private void ReadyToJump(bool mode)
    {
        foreach (var trig in Triggers)
        {
            trig.autoAction = mode;
        }
    }

    public void OnTriggerUsed()
    {
        _input.ExitInputZone();
        StartCoroutine(DisableInputZoneForTime(2f));
    }

    IEnumerator DisableInputZoneForTime(float time)
    {

        EnableInputZone(false);
        yield return new WaitForSeconds(time);

        EnableInputZone(true);
        ReadyToJump(false);
    }

    private void EnableInputZone(bool value)
    {
        foreach (var trig in Triggers)
        {
            trig.gameObject.SetActive(value);
        }
        var collider = GetComponent<Collider>();
        collider.enabled = value;
    }

    public void OnPalyerJump()
    {
        ReadyToJump(true);
    }
    
}
