using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Invector.CharacterController;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Assets.Scripts.Player.InvectorMods
{
    public class ParkourThirdPersonInput : vThirdPersonInput
    {
        public bool LockRunning = false;
        public bool LockTurning = false;
        public bool Sprint = false;


        public bool DebugAllowFreeWalk = false;

        private bool _isInInputZone = false;
        private ObstacleInputZone _inputZone;


        protected override void MoveCharacter()
        {

            if (DebugAllowFreeWalk)
            {
                oldInput = cc.input;
                cc.input.y = CrossPlatformInputManager.GetAxis("Vertical");
                cc.input.x = CrossPlatformInputManager.GetAxis("Horizontal");
                return;
            }

            if (!lockInput)
            {
                //cc.input.y = LockRunning ? 0f : 1f;
                //cc.input.x = LockTurning ? 0f : horizontalInput.GetAxis();

                cc.input.y = LockRunning ? 0f : 1f;
                cc.input.x = LockTurning ? 0f : CrossPlatformInputManager.GetAxis("Horizontal");
            }
            else
            {
                cc.input = Vector2.zero;
            }

            // update oldInput to compare with current Input if keepDirection is true
            // if (!keepDirection)
            //oldInput = cc.input;
        }

        protected override void SprintInput()
        {
            if (Sprint)
                cc.Sprint(true);
            else
                cc.Sprint(false);
        }

        protected override void CrouchInput()
        {
            //Запрещаем приседать
            /*if (crouchInput.GetButtonDown())
                cc.Crouch();*/
        }

        //Прыжок так же выполняет функцию трюка перед препятствием
        protected override void JumpInput()
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump") || jumpInput.GetButtonDown())
            {
                if (!_isInInputZone)
                {
                    cc.Jump();
                }
                else
                {
                    HUDController.Instance.Flash();
                    _inputZone.OnPalyerJump();
                }
            }
        }

        public void EnterInputZone(ObstacleInputZone zone)
        {
            _inputZone = zone;
            LockTurning = true;
            _isInInputZone = true;
        }

        public void ExitInputZone()
        {
            LockTurning = false;
            _isInInputZone = false;
        }


    }

}
