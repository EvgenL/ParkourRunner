using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Player
{
    public static class TrickNames
    {
        public enum Roll
        {
            BasicRoll,
            SideRoll,
            QuickRoll,


            Slide,
            LowSlide
        }
        public enum Slide
        {
            Slide,
            LowSlide
        }
        public enum Jump
        {
            Jump
        }
        public enum ClimbUpFar
        {
            ClimbUp3mFar,
            VerticalWallRun
        }
        public enum ClimbUpClose
        {
            ClimbUp2mClose,
            ClimbUp3mClose,
            ClimbUp4mClose
        }
        public enum ClimbUp1mClose
        {
            StepUp
        }
        public enum ClimbUp1mFar
        {
            MantleClimb1m,
            MantleClimb_Frontsault
        }
        public enum JumpOverClose
        {
            JumpOver
        }
        public enum JumpOverFar
        {
            FrontFlip,
            KongVault,
            OneHandVault,
            BigJump,
            BackVault,
            SwingVault,
            SiderollVault,
            JumpOverTwoHand
        }
        public enum JumpOver2m///not used
        {
            Wall_2M
        }
        public enum WallRunLeft///not used
        {
            WallRunLeft
        }

        public enum Stand
        {
            Webster
        }
    }
}
