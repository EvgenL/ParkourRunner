﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Generator
{
    [ExecuteInEditMode]
    public class Block : MonoBehaviour
    {
        public enum BlockType
        {
            Challenge,
            Relax,
            Start
        }
        
        public List<GenerationPoint> GenerationPoints;
        public BlockType Type;
        public Block Next;
                
        //Если это будет лагать - удалим
        private void Awake()
        {
            GenerationPoints = GetComponentsInChildren<GenerationPoint>().ToList();
        }

        //Автоматически расставляем референсы в эдиторе
        void Update()
        {
            if (!Application.isPlaying)
            {
                // GenerationPoints = GetComponentsInChildren<GenerationPoint>().ToList();
            }
        }

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
    }
}