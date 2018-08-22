using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using RootMotion.Dynamics;
using UnityEngine;

public enum Bodypart
{
    Head,
    Body,
    LHand,
    RHand,
    LLeg,
    RLeg
}

public class MuscleDismember : MonoBehaviour
{
    public GameObject NormalVersion;
    public GameObject DestroyedVersion;

    public bool IsDismembered = false;
    [SerializeField]
    private Bodypart _bodypart;

    public bool IsHandOrFeet = false; //При отчленении крайней части (ладонь, стопа) отваливается перыдущая (локоть, колено) потому что у нас нет такого количества моделек, да и это не нужно
    public MuscleDismember PreviousDismember;


    public void DisableNormal()
    {
            NormalVersion.SetActive(false);
    }

    public void EnableDestroyed()
    {
            DestroyedVersion.SetActive(true);
    }

    public void DismemberThis()
    {
        var broadcaster = GetComponent<MuscleCollisionBroadcaster>();
        if (broadcaster != null)
            DismemberMuscleRecursive(broadcaster);
    }

    public void DismemberMuscleRecursive(MuscleCollisionBroadcaster broadcaster)
    {
        if (_bodypart == Bodypart.Body) return;
        if (!GameManager.Instance.PlayerCanBeDismembered) return;
        if (IsDismembered) return;
        if (IsHandOrFeet)
        {
            PreviousDismember.DismemberThis();
            return;
        }
        var puppetMaster = broadcaster.puppetMaster;
        var joint = broadcaster.puppetMaster.muscles[broadcaster.muscleIndex].joint;
        var muscles = puppetMaster.muscles;
        int index = puppetMaster.GetMuscleIndex(joint);


        //News
        var newLimb = Instantiate(broadcaster.transform, broadcaster.transform.position, broadcaster.transform.rotation);
        var newBroadcaster = newLimb.GetComponent<MuscleCollisionBroadcaster>();
        var newJoint = newBroadcaster.puppetMaster.muscles[broadcaster.muscleIndex].joint;
        newJoint = newBroadcaster.GetComponent<ConfigurableJoint>();

        if (newJoint.connectedBody != null)
        {
            Destroy(newJoint);
            Destroy(this);
        }

        newLimb.GetComponent<Collider>().material = null; //Чтоб не скользило по земле

        //Этио мышцы, котоые идут дальше по конечности (плечо->локоть->ладонь)
        var newDismembers = newLimb.GetComponentsInChildren<MuscleDismember>();
        foreach (var dism in newDismembers)
        {
            if (!dism.IsHandOrFeet && dism.NormalVersion.activeInHierarchy) //Является ли эта конечность последней (для них нет destroyedVersion) и не была ли она оторвана ещё раньше?
            {
                dism.EnableDestroyed();
            }
            else
            {
                //TODO Удалить физику с невидимой части конечности (не особо заметный эффект)
                //var rb = dism.GetComponent<Rigidbody>();
                //Destroy(rb);
                //var collider = dism.GetComponent<Collider>();
                //Destroy(collider);
            }

            //Записываем что оторвали конечность
            GameManager.Instance.Limb(_bodypart, false);
            Destroy(dism);
        }


        //Olds
        var dismember = muscles[index].transform.GetComponent<MuscleDismember>();
        if (dismember != null)
        {
            dismember.DisableNormal();
        }
        for (int i = 0; i < muscles[index].childIndexes.Length; i++)
        {
            dismember = muscles[muscles[index].childIndexes[i]].transform.GetComponent<MuscleDismember>();
            if (dismember != null && !dismember.IsHandOrFeet)
            {
                dismember.DisableNormal();
            }
        }

        IsDismembered = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsDismembered) return;
        if (collision.transform.gameObject.layer == gameObject.layer) return;

        if (collision.relativeVelocity.magnitude > 
            GameManager.Instance.VelocityToDismember)
        {
            DismemberMuscleRecursive(GetComponent<MuscleCollisionBroadcaster>());
        }
    }






}
