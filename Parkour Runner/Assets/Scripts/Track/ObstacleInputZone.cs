using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Player.InvectorMods;
using Invector.CharacterController;
using UnityEngine;


public class ObstacleInputZone : MonoBehaviour
{

    public bool ReadJumpInput = true;
    public bool ReadRollInput = false;

    public List<vTriggerGenericAction> JumpTriggers = new List<vTriggerGenericAction>();
    public List<vTriggerGenericAction> RollTriggers = new List<vTriggerGenericAction>();

    //public float DisableTriggersForSeconds = 2f;

    private void Awake()
    {
        //Чтоб наш геймдизайнер руками не добавлял в листы
        if (transform.parent.childCount > 1)
        JumpTriggers = transform.parent.GetComponentsInChildren<vTriggerGenericAction>()
            .Where((x) => x.playAnimation != ("Roll")).ToList();
    }

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
        ReadyRoll(false);
        ReadyJump(false);
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
