using System.Collections.Generic;
using System.Linq;
using Basic_Locomotion.Scripts.CharacterController;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Generator
{
    public class Building : MonoBehaviour {

        /*public GenerationPoint[] GPoints;

        [SerializeField] private int StandTricksCount;
        [SerializeField] private int BonusCount;
        private Transform _player;
        private LevelGenerator _lg;

        private void Awake()
        {
            _lg = LevelGenerator.Instance;
            _player = FindObjectOfType<vThirdPersonController>().transform;
        }

        //Автоматически расставляем референсы в эдиторе
        public void UpdateReferences()
        {
            GPoints = GetComponentsInChildren<GenerationPoint>();
        }




        public void Clear()
        {
            foreach (var point in GPoints)
            {
                point.Clear();
            }

            StandTricksCount = 0;
            BonusCount = 0;
        }

        public void Generate()
        {
            foreach (var point in GPoints)
            {
                GeneratoOnPoint(point);
            }
        }

        private void GeneratoOnPoint(GenerationPoint point)
        {
            var pointPos = point.transform.position; //TODO Возможный источник бага двойной генерации

            if (!PointInZone(pointPos)) return;

            //Если точка находится в нашей зоне, и не слишком далеко от игрока по оси х
            if (point is Obstacle)
            {
                GenerateObstacle(point);
            }
            else if (point is StandTrick)
            {
                GenerateStand(point);
            }
            else //if PickUp
            {
                GeneratePickUp(point);
            }
        }

        private void GeneratePickUp(GenerationPoint point)
        {
//TODO ограничить количество на каждый дом
            if (BonusCount < _lg.BonusPerBuilding && Random.Range(0f, 1f) < _lg.BonusChance)
            {
                point.Generate();
                point.Used = true;
                Debug.DrawRay(point.transform.position, Vector3.up * 5f, Color.green, 5f);
                BonusCount++;
            }
            else
            {
                point.Used = true;
                Debug.DrawRay(point.transform.position, Vector3.up * 5f, Color.red, 5f);
            }
        }

        private void GenerateStand(GenerationPoint point)
        {
            if (_lg.State == LevelGenerator.GeneratorState.Relax)
            {
                if (StandTricksCount < _lg.StandTricksPerBuilding) //Random.Range(0f, 1f) < StaticConst.RelaxTrickPercent)
                {
                    point.Generate();
                    point.Used = true;
                    Debug.DrawRay(point.transform.position, Vector3.up * 5f, Color.green, 5f);
                    StandTricksCount++;
                }
                else
                {
                    point.Used = true;
                    Debug.DrawRay(point.transform.position, Vector3.up * 5f, Color.red, 5f);
                }
            }
        }

        private void GenerateObstacle(GenerationPoint point)
        {
//TODO ограничить количество на каждый дом
            if (Random.Range(0f, 1f) < _lg.ObstacleChance)
            {
                point.Generate();
                point.Used = true;
                Debug.DrawRay(point.transform.position, Vector3.up * 5f, Color.green, 5f);
            }
            else
            {
                point.Used = true;
                Debug.DrawRay(point.transform.position, Vector3.up * 5f, Color.red, 5f);
            }
        }

        //Если точка находится в нашей зоне, и не слишком далеко от игрока по оси х
        private bool PointInZone(Vector3 pointPos)
        {
            var playerPosX = _player.position.x;

            float stateLength = _lg.StateLength;
            float positionZ = _lg.transform.position.z;

            return pointPos.z > positionZ
                   && pointPos.z < positionZ + stateLength
                   && pointPos.x > playerPosX - _lg.ObstacleGenerationWidth
                   && pointPos.x < playerPosX + _lg.ObstacleGenerationWidth;
        }*/
    }
}
