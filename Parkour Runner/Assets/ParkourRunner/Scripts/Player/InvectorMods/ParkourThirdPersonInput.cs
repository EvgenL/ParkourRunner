﻿using Basic_Locomotion.Scripts.CharacterController;
using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Track;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace ParkourRunner.Scripts.Player.InvectorMods
{
    public class ParkourThirdPersonInput : vThirdPersonInput
    {
        public bool LockRunning = false;
        public bool LockTurning = false;
        public bool Sprint = false;

        public bool DebugAllowFreeWalk = false;
        
        private bool _isInInputZone = false;
        private ObstacleInputZone _inputZone;

        private ParkourThirdPersonController parkourController;

        private new void Start()
        {
            base.Start();
            parkourController = GetComponent<ParkourThirdPersonController>();
        }

        protected override void MoveCharacter()
        {
            if (parkourController.IsSlidingDown)
            {
                cc.input.y = 1f;
                cc.input.x = CrossPlatformInputManager.GetAxis("Horizontal");
                //oldInput = cc.input;
                return;
            }
            if (parkourController.IsSlidingTrolley)
            {
                cc.input.y = 0f;
                cc.input.x = CrossPlatformInputManager.GetAxis("Horizontal"); //TODO Наклон на троллеи
                //oldInput = cc.input;
                return;
            }
            if (DebugAllowFreeWalk)
            {
                cc.input.y = CrossPlatformInputManager.GetAxis("Vertical");
                cc.input.x = CrossPlatformInputManager.GetAxis("Horizontal");
                //oldInput = cc.input;
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

            oldInput = cc.input;
            // update oldInput to compare with current Input if keepDirection is true
            // if (!keepDirection)
            //oldInput = cc.input;
        }

        protected override void SprintInput()
        {
            if (Sprint || Input.GetKeyDown(KeyCode.LeftShift))
                cc.Sprint(true);
            else
                cc.Sprint(false);
        }

        protected override void CrouchInput()
        {
            //Запрещаем приседать потому что в игре не будет приседа
            /*if (crouchInput.GetButtonDown())
                cc.Crouch();*/
        }

        //Прыжок так же выполняет функцию трюка перед препятствием
        protected override void JumpInput()
        {
            if (CrossPlatformInputManager.GetButtonDown("Jump") || jumpInput.GetButtonDown())
            {
                Jump();
            }
        }

        public void Jump()
        {
            if (_isInInputZone && _inputZone.ReadJumpInput)
            {
                _inputZone.OnPalyerJump();
            }
            else if (parkourController.IsSlidingTrolley)
            {
                parkourController.IsSlidingTrolley = false;
            }
            else if (parkourController.IsUsingHook)
            {
                parkourController.IsUsingHook = false;
            }
            else if (parkourController.IsRunningWall)
            {
                //TODO разные виды джампоффа, климб
                parkourController.animator.SetTrigger("JumpOffWallTrigger");
                parkourController.IsRunningWall = false;
            }
            else
            {
                cc.Jump();
            }
        }

        protected override void RollInput()
        {
            if (CrossPlatformInputManager.GetButtonDown("Roll") || rollInput.GetButtonDown())
            {
                Roll();
            }
        }

        public void Roll()
        {
            if (_isInInputZone && _inputZone.ReadRollInput)
            {
                _inputZone.OnPalyerRoll();
            }
            else if (parkourController.IsSlidingTrolley)
            {
                parkourController.IsSlidingTrolley = false;
            }
            else if (parkourController.IsRunningWall)
            {
                //TODO разные виды джампоффа, климб
                GameManager.Instance.PlayerCanBeDismembered = true;
                parkourController.animator.SetTrigger("JumpOffWallTrigger");
                parkourController.IsRunningWall = false;
            }
            else
            {
                cc.Roll();
            }
        }

        public void EnterInputZone(ObstacleInputZone zone)
        {
            _inputZone = zone;
            _isInInputZone = true;

            LockTurning = true;
        }

        public void ExitInputZone()
        {
            _isInInputZone = false;

            LockTurning = false;
        }

        public override void CameraInput()
        {
            if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
            if (!ignoreCameraRotation)
            {
                //if (!keepDirection) cc.UpdateTargetDirection(Camera.main.transform);
                //Теперь игрок бежит всегда вперёд, независимо от поворота камеры.
                cc.UpdateTargetDirection(Camera.main.transform);
                RotateWithCamera(Camera.main.transform);
            }
            else
            {
                cc.UpdateTargetDirection(transform.root);
            }

            if (tpCamera == null)
                return;

            var Y = lockCameraInput ? 0f : rotateCameraYInput.GetAxis();
            var X = lockCameraInput ? 0f : rotateCameraXInput.GetAxis();
            var zoom = cameraZoomInput.GetAxis();

            tpCamera.RotateCamera(X, Y);
            tpCamera.Zoom(zoom);

            // change keedDirection from input diference
            if (keepDirection && Vector2.Distance(cc.input, oldInput) > 0.2f) keepDirection = false;
        }
    }
}