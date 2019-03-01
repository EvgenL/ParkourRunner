using RootMotion.Dynamics;
using UnityEngine;

namespace ParkourRunner.Scripts.Player
{
    public class PuppetMasterHighFall : MonoBehaviour
    {
        [HideInInspector]
        public static PuppetMasterHighFall Instance;

        void Start()
        {
            Instance = this;
        }

        public float Delay = 1f;

        public PuppetMaster PuppetMaster;

        public void DieTemporary()
        {
            //PuppetMaster.state = PuppetMaster.State.Dead;
            // Invoke("Revive", Delay);
        }

        private void Revive()
        {
            //PuppetMaster.state = PuppetMaster.State.Alive;
        }

    }
}
