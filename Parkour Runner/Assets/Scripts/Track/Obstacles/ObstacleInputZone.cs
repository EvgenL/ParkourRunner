using System.Collections;
using System.Collections.Generic;
using Invector.CharacterController;
using UnityEngine;

public class ObstacleInputZone : MonoBehaviour {

    public List<vTriggerGenericAction> Triggers = new List<vTriggerGenericAction>();
    public List<vObjectDamage> Killers = new List<vObjectDamage>();
    
    public vCharacter Character;

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

        vCharacter c = other.GetComponent<vCharacter>();
        if (c == null) return;
        Character = c;
    }



    private void OnTriggerStay(Collider other)
    {
        //TODO InputManager
        if (Input.GetKeyDown(KeyCode.R))
        {
            HUDController.Instance.Flash();
            Character.GetComponent<KeyboardInput>().LockStrafe = true; //temp
            StartCoroutine(WaitForRagdollStandUp());
        }
    }

    //Это я делаю для того чтобы персонаж, пребывая в регдоле, не мог сверхестественно преодолель препятствие
    IEnumerator WaitForRagdollStandUp()
    {
        while (Character.ragdolled)
        {
            yield return null;
        }
        ReadyToJump(true);
    }

    private void OnTriggerExit(Collider other)
    {
        Character.GetComponent<KeyboardInput>().LockStrafe = false; //temp
        //ReadyToJump(false);
    }

    private void ReadyToJump(bool mode)
    {
        foreach (var trig in Triggers)
        {
            trig.autoAction = mode;
        }
        foreach (var trig in Killers)
        {
            trig.gameObject.SetActive(!mode);
        }
    }

    public void OnTriggerUsed()
    {
        Character.GetComponent<KeyboardInput>().LockStrafe = false; //temp
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
        foreach (var trig in Killers)
        {
            trig.gameObject.SetActive(value);
        }
        var collider = GetComponent<Collider>();
        collider.enabled = value;
    }

}
