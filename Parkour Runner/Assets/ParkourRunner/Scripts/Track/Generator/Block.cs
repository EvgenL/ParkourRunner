using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Generator
{
    [ExecuteInEditMode]
    public class Block : MonoBehaviour
    {
        //public Building[] Buildings;
        public int BonusesOnThisBlock = 1;

        public List<GenerationPoint> GenerationPoints;

        //Автоматически расставляем референсы в эдиторе
        void Update()
        {
            if (!Application.isPlaying)
            {
                  GenerationPoints = GetComponentsInChildren<GenerationPoint>().ToList();
            }
        }

        public enum BlockType
        {
            Challenge,
            Relax,
            Start
        }

        public BlockType Type;
        public Block Next;
        public Block Prev;
        //public Block Left;
        //public Block Right;


        public void Generate()
        {
            if (GenerationPoints.Count == 0) return;
            for (int i = 0; i < BonusesOnThisBlock; i++)
            {
                int randI = Random.Range(0, GenerationPoints.Count);
                if (randI < 0) continue;
                GenerationPoints[randI].Generate();
                GenerationPoints.RemoveAt(randI);
            }

            /*foreach (var building in Buildings)
            {
                building.Generate();
            }*/
        }

       /*public void Clear()
        {
            foreach (var building in Buildings)
            {
                building.Clear();
            }
        }*/
    }
}
