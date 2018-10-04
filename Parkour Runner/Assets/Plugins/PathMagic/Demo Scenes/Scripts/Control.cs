using PathMagic.Scripts;
using UnityEngine;

namespace PathMagic.Demo_Scenes.Scripts
{
    public class Control : MonoBehaviour
    {
        public PathMagicAnimator pma;

        // Use this for initialization
        void Start ()
        {
	
        }
	
        // Update is called once per frame
        void Update ()
        {
            pma.VelocityBias = Input.GetAxis ("Horizontal") / 2f;
        }
    }
}
