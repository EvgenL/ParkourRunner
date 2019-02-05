using System.Collections.Generic;
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

        public enum EnvironmentKinds
        {
            Default,
            Tunnel
        }

        public List<GenerationPoint> GenerationPoints;
        [SerializeField] private EnvironmentKinds _environment;
        public BlockType Type;
        public Block Next;

        public EnvironmentKinds Environment { get { return _environment; } }

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

        /*public void Clear()
        {
            foreach (var building in Buildings)
            {
                building.Clear();
            }
        }*/
    }
}
