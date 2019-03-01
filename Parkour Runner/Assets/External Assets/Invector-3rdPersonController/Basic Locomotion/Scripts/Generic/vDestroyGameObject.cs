using System.Collections;
using UnityEngine;

namespace Basic_Locomotion.Scripts.Generic
{
    public class vDestroyGameObject : MonoBehaviour
    {
        public float delay;

        IEnumerator Start()
        {
            yield return new WaitForSeconds(delay);
            Destroy(gameObject);
        }
    }
}
