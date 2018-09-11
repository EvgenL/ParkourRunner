using System;

namespace ParkourRunner.Scripts.Player
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

                case ("JumpOverFar"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.JumpOverFar)).Length);
                    return ((TrickNames.JumpOverFar)randomIndex).ToString();

                case ("JumpOverClose"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.JumpOverClose)).Length);
                    return ((TrickNames.JumpOverClose)randomIndex).ToString();

                case ("Stand"):
                    randomIndex = UnityEngine.Random.Range(0, Enum.GetValues(typeof(TrickNames.Stand)).Length);
                    return ((TrickNames.Stand)randomIndex).ToString();



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
