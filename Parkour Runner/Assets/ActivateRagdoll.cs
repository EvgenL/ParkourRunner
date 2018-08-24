using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using UnityEngine;

public class ActivateRagdoll : StateMachineBehaviour {

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        var behav = FindObjectOfType<PuppetMasterHighFall>();
        behav.DieTemporary();
    }






}
