using System.Collections;
using System.Collections.Generic;
using Invector.CharacterController;
using RootMotion.Dynamics;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    public float VetricalForce;
    public float UnpinForce;

    private Rigidbody _playerRb;

    private void Start()
    {
        //Кидает нулл референс почему то
        //_playerRb = vThirdPersonController.instance.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
            _playerRb = vThirdPersonController.instance.GetComponent<Rigidbody>();
            _playerRb.AddForce(0, VetricalForce, 0, ForceMode.Force);
            collision.transform.GetComponent<Rigidbody>().AddForce(0, UnpinForce, 0, ForceMode.Force);
        //TODO .Unpin();
    }
}
