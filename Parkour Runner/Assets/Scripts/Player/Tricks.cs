using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Player
{
    public static class Tricks
    {
        public static string GetTrick(string playAnimation)
        {
            int randomIndex;
            switch (playAnimation)
            {
                case ("Roll"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.Roll)).Length);
                    return ((TrickNames.Roll)randomIndex).ToString(); //Получаем случайную строку из енума

                case ("Jump"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.Jump)).Length);
                    return ((TrickNames.Jump)randomIndex).ToString(); //Получаем случайную строку из енума

                case ("ClimbUpFar"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.ClimbUpFar)).Length);
                    return ((TrickNames.ClimbUpFar)randomIndex).ToString();

                case ("ClimbUpClose"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.ClimbUpClose)).Length);
                    return ((TrickNames.ClimbUpClose)randomIndex).ToString();

                case ("JumpOverFar"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.JumpOverFar)).Length);
                    return ((TrickNames.JumpOverFar)randomIndex).ToString();

                case ("JumpOverClose"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.JumpOverClose)).Length);
                    return ((TrickNames.JumpOverClose)randomIndex).ToString();


                default:
                    UnityEngine.Debug.Log("No such trick category: " + playAnimation);
                    return playAnimation; //Для actions, которые не могут быть рандомными
            }

            //TODO Зависисость от открытых трюков
            
        }

        public static string GetRandomRoll()
        {
            return GetTrick("Roll");
        }
    }

    public static class TrickNames
    {
        public enum Roll
        {
            BasicRoll,
            SideRoll,
            QuickRoll,


            SlideRoll,
            CorkscrewKipUp
        }
        public enum Slide
        {
            SlideRoll,
            CorkscrewKipUp
        }
        public enum Jump
        {
            Jump
        }
        public enum ClimbUpFar
        {
            ClimbUpFar
        }
        public enum ClimbUpClose
        {
            ClimbUpClose2m,
            ClimbUpClose3m
        }
        public enum JumpOverFar
        {
            BasicJump,
            Frontsault,
            Gainer,
            Double_Frontsault,
            KongVault,
            OneHandVault
        }
        public enum JumpOverClose
        {
            JumpOver,
            Cartwheel
        }
    }
}
