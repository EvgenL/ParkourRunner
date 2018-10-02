using System;
using System.Collections;
using System.Collections.Generic;
using ParkourRunner.Scripts.Player.InvectorMods;
using RootMotion.Dynamics;
using TMPro;
using UnityEngine;

public class ParkourCamera : MonoBehaviour
{

    private enum UpdateType
    {
        Update,
        FixedUpdate,
        LateUpdate
    }

    public Vector3 Offset;
    public bool SetOffsetAtStart = true;
    public Vector3 TrickOffset;

    [Range(0f, 1f)] public float FollowSmooth = 0.7f;
    [Range(0f, 1f)] public float AngleSmooth = 0.7f;

    [SerializeField] private UpdateType _updateType;

    [SerializeField] private PuppetMaster _puppetMaster;

	// Use this for initialization
	void Start () {
	    if (_puppetMaster == null)
	    {
	        _puppetMaster = FindObjectOfType<PuppetMaster>();
	    }

	    if (SetOffsetAtStart)
	    {
	        Offset = -_puppetMaster.muscles[0].transform.position + transform.position;
        }
	}

    private void Update()
    {
        if (_updateType == UpdateType.Update)
            UpdatePosition();
    }
    private void FixedUpdate()
    {
        if (_updateType == UpdateType.FixedUpdate)
            UpdatePosition();
    }
    private void LateUpdate()
    {
        if (_updateType == UpdateType.LateUpdate)
            UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 middle = GetMiddlePos();
        //transform.position = Vector3.Lerp(middle + Offset, transform.position, FollowSmooth);
        transform.position = Vector3.Lerp(
            FindObjectOfType<ParkourThirdPersonController>().transform.position + Offset + TrickOffset, 
            transform.position, FollowSmooth);


        Vector3 lookDir = GetLookDir(middle);
        Quaternion toRot = Quaternion.FromToRotation(transform.forward, lookDir);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRot, AngleSmooth);
    }

    private Vector3 GetLookDir(Vector3 mid)
    {
        Vector3 target = mid;
        target.x += Offset.x;
        return target - transform.position;
    }

    private Vector3 GetMiddlePos()
    {
        Vector3 pos = Vector3.zero;

        foreach (var muscle in _puppetMaster.muscles)
        {
            var musclePos = muscle.transform.position;
            pos += musclePos;
        }

        pos /= _puppetMaster.muscles.Length;
        
        return pos;
    }

    public void OnRoll()
    {
        StartCoroutine(Roll());
    }

    private IEnumerator Roll()
    {
        TrickOffset = new Vector3(0, -1f, 0);
        yield return new WaitForSeconds(1f);
        TrickOffset = new Vector3(0, 0f, 0);
    }
    public void OnJump()
    {

    }

    public void OnUnpin()
    {

    }
    public void OnRegainBalance()
    {

    }

    public void OnHeadLost(Transform head)
    {

    }

    public void OnHeadRegenerated()
    {

    }
}
