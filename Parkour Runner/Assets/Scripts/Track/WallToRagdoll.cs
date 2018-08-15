
using System.Collections;
using Invector.CharacterController;
using UnityEngine;

namespace Assets.Scripts.InvectorMods
{
    class WallToRagdoll : vObjectDamage
    {
        void Start()
        {
            GetComponent<Collider>().isTrigger = true;
            damage.activeRagdoll = true;
            if (!tags.Contains("Player")) tags.Add("Player");
            gameObject.layer = LayerMask.NameToLayer("Triggers");
        }

        new void OnTriggerEnter(Collider hit)
        {
            if (hit.tag != "Player") return;
            if (hit.GetComponent<vCharacter>() == null) return;

            print("Get ragdolled, birch");

            base.OnTriggerEnter(hit);

            StartCoroutine(Cooldown(2f));
        }

        private IEnumerator Cooldown(float f)
        {
            enabled = false;
            yield return new WaitForSeconds(f);
            enabled = true;
        }

        private void OnTriggerStay(Collider hit)
        {
            OnTriggerEnter(hit);
        }
    }
}
