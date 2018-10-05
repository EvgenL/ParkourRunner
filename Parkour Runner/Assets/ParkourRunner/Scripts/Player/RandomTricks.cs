using System;
using System.Collections.Generic;
using System.Linq;
using ParkourRunner.Scripts.Managers;
using UnityEngine;

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
                    return ProgressManager.Instance.GetRandomJumpOver().AnimationName;

                case ("JumpOverClose"):
                    return "JumpOver";

                case ("Stand"):

                    //TODO proc reward
                    
                    List<Trick> stands = ResourcesManager.StandTricks.FindAll(x => x.IsBought);
                    randomIndex = UnityEngine.Random.Range(0, stands.Count);
                    Debug.Log("RI = " + randomIndex + ". name = " + stands[randomIndex].Name);
                    Trick randomTrick = stands[randomIndex];
                    Debug.Log("Stand: " + randomTrick.Name + ". Reward: " + randomTrick.MoneyReward);
                    GameManager.Instance.AddCoin(randomTrick.MoneyReward);
                    return randomTrick.AnimationName;



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
