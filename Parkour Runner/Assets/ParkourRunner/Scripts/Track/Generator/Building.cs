using System.Collections.Generic;
using System.Linq;
using Basic_Locomotion.Scripts.CharacterController;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Generator
{
    public class Building : MonoBehaviour {

        public List<GenerationPoint> GPoints = new List<GenerationPoint>();

        [SerializeField] private int StandTricksCount;
        [SerializeField] private int BonusCount;
        private Transform _player;

        private void Awake()
        {
            _player = FindObjectOfType<vThirdPersonController>().transform;
        }

        //Автоматически расставляем референсы в эдиторе
        public void UpdateReferences()
        {
            GPoints = GetComponentsInChildren<GenerationPoint>().ToList();
        }


        public void Generate()
        {
            var playerPosX = _player.position.x;
            LevelGenerator lg = LevelGenerator.Instance;
            float stateLength = lg.StateLength;
            float positionZ = lg.transform.position.z;

            foreach (var point in GPoints)
            {
                var pointPos = point.transform.position; //TODO Возможный источник бага двойной генерации

                //Если точка находится в нашей зоне, и не слишком далеко от игрока по оси х
                if (pointPos.z > positionZ
                    && pointPos.z < positionZ + stateLength
                    && pointPos.x > playerPosX - lg.ObstacleGenerationWidth
                    && pointPos.x < playerPosX + lg.ObstacleGenerationWidth)
                {
                    if (point is Obstacle)
                    {
                        //TODO ограничить количество на каждый дом
                        if (Random.Range(0f, 1f) < lg.ObstacleChance)
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
                    else if (point is StandTrick)
                    {
                        //TODO ограничить количество на каждый дом!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        if (lg.State == LevelGenerator.GeneratorState.Relax)
                        {
                            if (StandTricksCount < lg.StandTricksPerBuilding) //Random.Range(0f, 1f) < StaticConst.RelaxTrickPercent)
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
                    else //if PickUp
                    {
                        //TODO ограничить количество на каждый дом
                        if (BonusCount < lg.BonusPerBuilding && Random.Range(0f, 1f) < lg.BonusChance)
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
                }
            }
        }
    }
}
