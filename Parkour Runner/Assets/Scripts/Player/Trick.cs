using UnityEngine;

namespace ParkourRunner.Scripts.Player
{
    [CreateAssetMenu(menuName = "ParkouRunner/Trick", fileName = "New Trick")]
    public class Trick : ScriptableObject
    {
        public string Name = "Trick";
        public string AnimationName = "Trick Animiation";

        public int Cost;    //Цена в магазине
        public bool IsBought;   //Куплен?

        public int MoneyReward; //Награда за выполнение
    }
}
