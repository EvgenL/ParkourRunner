using System.Collections;
using Basic_Locomotion.Scripts.Generic;
using UnityEngine;

namespace Basic_Locomotion.Scripts.CharacterController.Examples._2._5D
{
    [vClassHeader("2.5D Input")]
    public class v2_5DInput : vThirdPersonInput
    {
        public v2_5DPath path;
        Vector2 joystickMousePos;
        Vector3 lookDirection;

        protected override void Start()
        {
            base.Start();

            path = FindObjectOfType<v2_5DPath>();
            if (path) StartCoroutine(InitPath());
        }

        IEnumerator InitPath()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            transform.position = path.ConstraintPosition(transform.position);
            cc.RotateToDirection(path.reference.right);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!cc.isDead && !cc.ragdolled)
                transform.position = Vector3.Lerp(transform.position, path.ConstraintPosition(transform.position), 80 * Time.deltaTime);

            transform.position = path.ConstraintPosition(transform.position);
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            if (!cc.isStrafing && path && cc.input.magnitude > 0.1f) cc.RotateToDirection(path.reference.right * cc.input.x);
        }
    }
}

