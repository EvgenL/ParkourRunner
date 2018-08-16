using System;

namespace Assets.Scripts.Player
{
    public static class RandomTricks
    {
        public static string GetTrick(string playAnimation)
        {
            int randomIndex;
            switch (playAnimation)
            {
                //TODO зависимость от открытых трюков
                case ("Roll"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.Roll)).Length);
                    return ((TrickNames.Roll)randomIndex).ToString(); //Получаем случайную строку из енума

                case ("Slide"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.Slide)).Length);
                    return ((TrickNames.Slide)randomIndex).ToString(); 

                case ("Jump"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.Jump)).Length);
                    return ((TrickNames.Jump)randomIndex).ToString();

                case ("ClimbUpFar"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.ClimbUpFar)).Length);
                    return ((TrickNames.ClimbUpFar)randomIndex).ToString();

                case ("ClimbUpClose"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.ClimbUpClose)).Length);
                    return ((TrickNames.ClimbUpClose)randomIndex).ToString();
                    
                case ("ClimbUp1mFar"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.ClimbUp1mFar)).Length);
                    return ((TrickNames.ClimbUp1mFar)randomIndex).ToString();

                case ("ClimbUp1mClose"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.ClimbUp1mClose)).Length);
                    return ((TrickNames.ClimbUp1mClose)randomIndex).ToString();

                case ("JumpOverFar"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.JumpOverFar)).Length);
                    return ((TrickNames.JumpOverFar)randomIndex).ToString();

                case ("JumpOverClose"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.JumpOverClose)).Length);
                    return ((TrickNames.JumpOverClose)randomIndex).ToString();

                case ("JumpOver2m"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.JumpOver2m)).Length);
                    return ((TrickNames.JumpOver2m)randomIndex).ToString();

                case ("Stand"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.Stand)).Length);
                    return ((TrickNames.Stand)randomIndex).ToString();  

                /*case ("WallRunLeft"): //TODO нет анимации!
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.WallRunLeft)).Length);
                    return ((TrickNames.WallRunLeft)randomIndex).ToString();*/


                default:
                    UnityEngine.Debug.Log("No such trick category: " + playAnimation + ". Trying to do exact trick");
                    return playAnimation; //Для actions, которые не могут быть рандомными
            }

            //TODO Зависисость от открытых трюков
            
        }

        public static string GetRandomRoll()
        {
            return GetTrick("Roll");
        }
    }

    
}
