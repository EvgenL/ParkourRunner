using ParkourRunner.Scripts.Managers;
using RootMotion.Dynamics;
using UnityEngine;

namespace ParkourRunner.Scripts.Player
{
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
        public Bodypart Bodypart;

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

        public void HealRecursive()
        {
            var dismembers = GetComponentsInChildren<MuscleDismember>();
            foreach (var dism in dismembers)
            {
                dism.Heal();
            }
        }

        private void Heal()
        {
            if (IsHandOrFeet) return;
            NormalVersion.SetActive(true);
            DestroyedVersion.SetActive(false);
            IsDismembered = false;
        }

        public void DismemberMuscleRecursive()
        {
            if (Bodypart == Bodypart.Body) return;
            if (!GameManager.Instance.PlayerCanBeDismembered)
                return;
            if (IsDismembered) return;
            if (IsHandOrFeet)
            {
                //Ладонь нельзя отчленить. Вместо этого оторвём руку по локоть
                PreviousDismember.DismemberMuscleRecursive();
                return;
            }
            var broadcaster = GetComponent<MuscleCollisionBroadcaster>();
            var puppetMaster = broadcaster.puppetMaster;
            var joint = broadcaster.puppetMaster.muscles[broadcaster.muscleIndex].joint;
            var muscles = puppetMaster.muscles;
            int index = puppetMaster.GetMuscleIndex(joint);


            //News
            var newLimb = Instantiate(broadcaster.transform, broadcaster.transform.position, broadcaster.transform.rotation);
            Destroy(newLimb.gameObject, 5f);
            var newBroadcaster = newLimb.GetComponent<MuscleCollisionBroadcaster>();
            var newJoint = //newBroadcaster.puppetMaster.muscles[broadcaster.muscleIndex].joint;
                            newBroadcaster.GetComponent<ConfigurableJoint>();

            if (newJoint.connectedBody != null)
            {
                Destroy(newJoint);
                //Destroy(this);
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

                if (dism.Bodypart == Bodypart.Head)
                {
                    GameManager.Instance.OnHeadLost(dism.transform);
                }
                else
                {
                    //TODO Удалить физику с невидимой части конечности (не особо заметный эффект)
                    /*
                    var j = dism.GetComponent<ConfigurableJoint>();
                    Destroy(j);
                    var rb = dism.GetComponent<Rigidbody>();
                    Destroy(rb);
                    var collider = dism.GetComponent<Collider>();
                    Destroy(collider);*/
                }

                //Записываем что оторвали конечность
                GameManager.Instance.SetLimbState(Bodypart, false);
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
            if (collision.transform.gameObject.layer == LayerMask.NameToLayer("DamageToRagdoll")) {
                if (collision.relativeVelocity.magnitude >
                    GameManager.Instance.VelocityToDismember)
                {
                    DismemberMuscleRecursive();
                }
            }
            //else if (collision.transform.gameObject.layer == LayerMask.NameToLayer("HouseWall")) {
            //        DismemberMuscleRecursive();
            //}
        }
    }
}