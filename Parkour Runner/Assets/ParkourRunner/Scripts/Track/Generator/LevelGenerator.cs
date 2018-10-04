﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Basic_Locomotion.Scripts.CharacterController;
using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Player.InvectorMods;
using UnityEngine;

namespace ParkourRunner.Scripts.Track.Generator
{
    public class LevelGenerator : MonoBehaviour
    {
        #region Singleton

        public static LevelGenerator Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
                return;
            }
            LoadPrefabs();
        }

        #endregion

        public int GenerateBlocksForward = 2;

        public enum GeneratorState
        {
            HeatUp, //Разогрев
            Callibration, //Каллибровка
            Reward, //Вознаграждение
            Challenge, //Трудности
            Relax //Отдых
        }

        public int BlockSide = 150; //Сколько метров сторона одного блока

        [SerializeField] private Vector3 StartBlockOffset;  //Позиция стартового блока

        public GeneratorState State;

        [SerializeField] private List<Block> _blockPool;

        [SerializeField] private Transform _player;


        /*public float StateLength
        {
            get
            {
                switch (State)
                {
                    case GeneratorState.HeatUp:
                        return HeatUpStateLength;

                    case GeneratorState.Callibration:
                        return CallibrationStateLength;

                    case GeneratorState.Relax:
                        return RelaxStateLength;

                    case GeneratorState.Reward:
                        return RewardStateLength;

                    case GeneratorState.Challenge:
                        return ChallengeStateLength;

                    default: return 0;
                }
            
            }
        }

        public double ObstacleChance
        {
            get
            {
                switch (State)
                {
                    case GeneratorState.HeatUp:
                        return StaticConst.HeatUpObstaclePercent;

                    case GeneratorState.Callibration:
                        return StaticConst.CallibrationObstaclePercent;

                    case GeneratorState.Relax:
                        return StaticConst.RelaxObstaclePercent;

                    case GeneratorState.Reward:
                        return StaticConst.RewardObstaclePercent;

                    case GeneratorState.Challenge:
                        return StaticConst.ChallengeObstaclePercent;

                    default: return 0;
                }
            }
        }

        public double BonusChance
        {
            get
            {
                switch (State)
                {
                    case GeneratorState.HeatUp:
                        return StaticConst.HeatUpPickUpPercent;

                    case GeneratorState.Callibration:
                        return StaticConst.CallibrationPickUpPercent;

                    case GeneratorState.Relax:
                        return StaticConst.RelaxPickUpPercent;

                    case GeneratorState.Reward:
                        return StaticConst.RewardPickUpPercent;

                    case GeneratorState.Challenge:
                        return StaticConst.ChallengePickUpPercent;

                    default: return 0;
                }
            }
        }
        */



        //Длины областей генерации препятствий
        [SerializeField] private float HeatUpStateLength = 20;
        [SerializeField] private float CallibrationStateLength = 20;
        [SerializeField] private float RewardStateLength = 15;
        [SerializeField] private float ChallengeStateLength = 25;
        [SerializeField] private float RelaxStateLength = 15;

        public int StandTricksPerBuilding = 1;
        public int BonusPerBuilding = 1;

        [SerializeField] private float ObstacleGenerationdistance = 100f;
        [SerializeField] private float BonusGenerationdistance = 100f;

        public float ObstacleGenerationWidth = 50f;

        public Block CenterBlock;

        private ResourcesManager _resourcesManager;
        private List<GameObject> _blockPrefabs;
        private List<GameObject> _challengeBlocks;
        private List<GameObject> _relaxBlocks;

        void Start ()
        {

            if (_blockPrefabs.Count == 0)
            {
                Debug.LogError("Не найдено ни одного блока в папке Resources/Blocks");
                Destroy(this);
                return;
            }

            if (_player == null)
            {
                _player = FindObjectOfType<ParkourThirdPersonController>().transform;
            }


            //reset pos
            //transform.position = _player.position + StartBlockOffset;
            //transform.position = _player.position + StartBlockOffset;

            GenerateStartBlock();
            StartCoroutine(Generate());
        }

        private void LoadPrefabs()
        {
//load resources
            _resourcesManager = ResourcesManager.Instance;
            _blockPrefabs = _resourcesManager.BlockPrefabs;
            _challengeBlocks = 
                _blockPrefabs.FindAll(
                    x => x.GetComponent<Block>()
                    .Type == 
                    Block.BlockType.Challenge);
            _relaxBlocks = _blockPrefabs.FindAll(x => x.GetComponent<Block>().Type == Block.BlockType.Relax);
        }

        private IEnumerator Generate()
        {
            while (true)
            {

                /*
                //TODO чередовать релакс и челендж
                if (State == GeneratorState.HeatUp)
                {
                    GenerateStartBlock();
                    CenterBlock = _blockPool[0];
                    State = GeneratorState.Challenge; //tODO
                }
                GenerateBlocks();
                //GenerateObstacles();
                */
                //CheckStates();
                Tick();
                yield return new WaitForSeconds(1f);
            }
        }



        private void GenerateStartBlock()
        {
            var startBlock = _blockPrefabs.Find(x => x.GetComponent<Block>().Type == Block.BlockType.Start);
            var startBlockGo = Instantiate(startBlock, _player.position + StartBlockOffset, Quaternion.identity);
            var startBlockScript = startBlockGo.GetComponent<Block>();
            _blockPool.Add(startBlockScript);
            //_blockPrefabs.Remove(startBlock);
            CenterBlock = startBlockScript;
            State = GeneratorState.HeatUp;
            GenerateBlocksAfter(startBlockScript);
        }

        private void Tick()
        {
            var newCenterBlock = NewCenterBlock();

            if (CenterBlock == newCenterBlock)
                return;

            GenerateBlocksAfter(newCenterBlock);
            DestroyOldBlocks();
            CenterBlock = newCenterBlock;
            //GenerateObstaclesOnNewBlocks();
        }

        private Block NewCenterBlock()
        {
            Block newCenterBlock = _blockPool[0];
            foreach (var block in _blockPool)
            {
                Vector3 blockPos = block.transform.position;

                Vector3 playerPos = _player.position;
                if (playerPos.z > blockPos.z - BlockSide / 2f && playerPos.z <= blockPos.z + BlockSide / 2f)
                {
                    newCenterBlock = block;
                    break;
                }
            }

            return newCenterBlock;
        }
        
        public void GenerateBlocksAfter(Block block, int i = 0)
        {
            Block nextBlockScript;
            if (block.Next == null)
            {
                var nextBlockPrefab = GetRandomBlock();
                var nextBlockGo = Instantiate(nextBlockPrefab,
                    block.transform.position + block.transform.forward * BlockSide, block.transform.rotation);
                nextBlockScript = nextBlockGo.GetComponent<Block>();
                _blockPool.Add(nextBlockScript);
                block.Next = nextBlockScript;
                block.Generate();
            }

            nextBlockScript = block.Next;

            i++;
            if (i < GenerateBlocksForward)
            {
                GenerateBlocksAfter(nextBlockScript, i);
            }
        }

        private void DestroyOldBlocks()
        {
            List<Block> blocksToDestroy = new List<Block>();
            foreach (var block in _blockPool)
            {
                if (block.transform.position.z < _player.position.z - (BlockSide/2f + 2f)) //Если игрок сощёл с предыдущего блока на 2 метра
                {
                    blocksToDestroy.Add(block);
                }
            }
            foreach (var block in blocksToDestroy)
            {
                _blockPool.Remove(block);
                Destroy(block.gameObject);
            }
        }
        


        public GameObject GetRandomBlock()
        {
            //TODO чередовать релакс и челленж
            return _challengeBlocks[Random.Range(0, _blockPrefabs.Count)];
        }
    }
    
}