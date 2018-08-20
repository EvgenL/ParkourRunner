using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player.InvectorMods;
using Invector.CharacterController;
using UnityEngine;


public class ObstacleInputZone : MonoBehaviour
{

    public bool ReadJumpInput = true;
    public bool ReadRollInput = false;

    public List<vTriggerGenericAction> JumpTriggers = new List<vTriggerGenericAction>();
    public List<vTriggerGenericAction> RollTriggers = new List<vTriggerGenericAction>();

    public float DisableTriggersForSeconds = 2f;

    private ParkourThirdPersonInput _input;
    private bool _isReady;

    private void Start()
    {
        foreach (var trig in JumpTriggers)
        {
            trig.OnDoAction.AddListener(OnTriggerUsed);
        }
        foreach (var trig in RollTriggers)
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
        ReadyRoll(false);
        ReadyJump(false);
    }

    private void ReadyRoll(bool mode)
    {
        //Когда зона получает инпут от игрока, на экране появляется вспышка
        if (mode && _isReady != mode)
        {
            HUDManager.Instance.Flash();
        }
        _isReady = mode;

        foreach (var trig in RollTriggers)
        {
            trig.autoAction = mode;
        }
    }

    private void ReadyJump(bool mode)
    {
        //Когда зона получает инпут от игрока, на экране появляется вспышка
        if (mode && _isReady != mode)
        {
            HUDManager.Instance.Flash();
        }
        _isReady = mode;

        foreach (var trig in JumpTriggers)
        {
            trig.autoAction = mode;
        }
    }

    public void OnTriggerUsed()
    {
        _input.ExitInputZone();
        if (DisableTriggersForSeconds == 0f) return;
        StartCoroutine(DisableInputZoneForTime(DisableTriggersForSeconds));
    }

    IEnumerator DisableInputZoneForTime(float time)
    {
        EnableInputZone(false);
        yield return new WaitForSeconds(time);

        EnableInputZone(true);
        ReadyRoll(false);
        ReadyJump(false);
    }

    private void EnableInputZone(bool value)
    {
        foreach (var trig in JumpTriggers)
        {
            trig.autoAction = value;
            //trig.gameObject.SetActive(value);
        }
        foreach (var trig in RollTriggers)
        {
            trig.autoAction = value;
            //trig.gameObject.SetActive(value);
        }
        var collider = GetComponent<Collider>();
        collider.enabled = value;
    }

    public void OnPalyerJump()
    {
        if (ReadJumpInput)
            ReadyJump(true);
    }
    public void OnPalyerRoll()
    {
        if (ReadRollInput)
            ReadyRoll(true);
    }

}
