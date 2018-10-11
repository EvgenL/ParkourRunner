using System.Collections.Generic;
using System.Linq;
using ParkourRunner.Scripts.Track.Pick_Ups.Bonuses;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Generator
{
    [ExecuteInEditMode]
    public class Block : MonoBehaviour
    {
        public List<GenerationPoint> GenerationPoints;

        //Автоматически расставляем референсы в эдиторе
        void Update()
        {
            if (!Application.isPlaying)
            {
                 // GenerationPoints = GetComponentsInChildren<GenerationPoint>().ToList();
            }
        }

        //Если это будет лагать - удалим
        private void Awake()
        {
            GenerationPoints = GetComponentsInChildren<GenerationPoint>().ToList();
        }

        public enum BlockType
        {
            Challenge,
            Relax,
            Start
        }

        public BlockType Type;
        public Block Next;


        public void Generate()
        {
            if (GenerationPoints.Count == 0) return;

            var coins = GenerationPoints.FindAll(x => x is CoinPoints);
            foreach (var c in coins)
            {
                c.Generate();
            }

            var tricks = GenerationPoints.FindAll(x => x is StandTrickGenerationPoint);
            GenerateSingle(tricks);

            var bonuses = GenerationPoints.FindAll(x => x is PickUpPoint);
            GenerateSingle(bonuses);
            }

        private void GenerateSingle(List<GenerationPoint> tricks)
        {
            if (tricks.Count > 0)
            {
                int randI = Random.Range(0, tricks.Count);
                GenerationPoints[randI].Generate();
            }
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
