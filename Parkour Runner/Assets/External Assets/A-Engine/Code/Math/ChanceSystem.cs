// A-Engine, Code version: 1

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AEngine
{
    public class ChanceSystem<T>
    {
        private List<T> list;
        private List<float> chanceWeightList;
        private List<float> chanceList;

        public static bool IsChanceByPercent(int percent)
        {
            if (Random.Range(1, 101) <= percent)
                return true;
            else
                return false;
        }

		public int Length
		{
			get { return list.Count; }
		}

        public ChanceSystem()
        {
            list = new List<T>();
            chanceWeightList = new List<float>();
            chanceList = new List<float>();
        }

        public void Add(T item, float chance)
        {
            list.Add(item);
            chanceWeightList.Add(0);
            chanceList.Add(chance);
        }

        public T Get()
        {
            if (list.Count == 0 || chanceList.Count == 0 || chanceWeightList.Count == 0)
            {
                Debug.Log("[Class = ChanceSystem, method = Get] : Chance list is empty.");
                return default(T);
            }

            float chance = Random.Range(0f, 100f);
            float minValue = 0;
            int index = 0;

            for (int i = 0; i < chanceWeightList.Count; i++)
            {
                if (minValue < chance && chance <= minValue + chanceWeightList[i])
                {
                    index = i;
                    break;
                }
                else if (minValue == 0 && chance == 0 && chance <= chanceWeightList[i])
                {
                    index = i;
                    break;
                }

                minValue += chanceWeightList[i];
            }

            return list[index];
        }

        public void Clear()
        {
            list.Clear();
            chanceWeightList.Clear();
            chanceList.Clear();
        }

        public void CalculateChanceWeights()
        {
            if (list.Count == 0 || chanceList.Count == 0 || chanceWeightList.Count == 0)
            {
                Debug.Log("[Class = ChanceSystem, method = CalculateChanceWeights] : Chance list is empty.");
                return;
            }

            float sum = 0;
            float percentValue = 0;

            for (int i = 0; i < chanceList.Count; i++)
                sum += chanceList[i];

            if (sum <= 0)
            {
                Debug.Log("[Class = ChanceSystem, method = CalculateChanceWeights] : Not correct percents in items.");
                return;
            }

            percentValue = sum / 100f;
            for (int i = 0; i < chanceWeightList.Count; i++)
                chanceWeightList[i] = chanceList[i] / percentValue;
        }
    }
}
