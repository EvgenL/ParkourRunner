using System.Collections;
using System.Collections.Generic;
using Invector.CharacterController;
using RootMotion.Dynamics;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    public float VetricalForce;
    public float UnpinForce;

    private void OnCollisionEnter(Collision collision)
    {

        //Это нога (возможно голова) которой игрок наступил на мину
        collision.transform.GetComponent<Rigidbody>()
            .AddForce(0, UnpinForce, 0, ForceMode.Force);

        var broadcaster =
            collision.transform.GetComponent<MuscleCollisionBroadcaster>();
        if (broadcaster != null)
        {
            //Находим тазовую кость и даём в неё addforce
            broadcaster.puppetMaster.muscles[0].transform.GetComponent<Rigidbody>()
                .AddForce(0, VetricalForce, 0, ForceMode.Force);

        }
        broadcaster.GetComponent<MuscleDismember>().DismemberThis();
        //gameObject.SetActive(false);

    }


}
