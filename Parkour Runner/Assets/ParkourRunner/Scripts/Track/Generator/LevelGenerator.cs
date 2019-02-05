﻿using System.Collections;
using System.Collections.Generic;
using ParkourRunner.Scripts.Managers;
using ParkourRunner.Scripts.Player.InvectorMods;
using Assets.ParkourRunner.Scripts.Track.Generator;
using UnityEngine;
using AEngine;

namespace ParkourRunner.Scripts.Track.Generator
{
    public class LevelGenerator : MonoBehaviour
    {
        private const int DEFAULT_INDEX = 0;

        #region Singleton
        public static LevelGenerator Instance;
        #endregion

        public int GenerateBlocksForward = 2;

        public enum GeneratorState
        {
           // HeatUp, //Разогрев
           // Callibration, //Каллибровка
           // Reward, //Вознаграждение
            Challenge, //Трудности
            Chill //
            //  /"""\   |   |   "|"  |    | 
            //  |       |---|    |   |    |
            //  \___/   |   |   .|.  |__  |__
            //

        }

        private enum EnvironmentGenerations
        {
            Default,        // Дефолтные блоки в определенном количестве
            Weight,         // Генерация дефолтных блоков (наибольший вес), пока не сработает вес специальных блоков
            Special         // Специальные блоки в определенном количестве (Robopolis, Tunnel)
        }

        private Res.DefaulEnvironmentSettings _defaultEnvironment;
        private List<Res.SpecialEnvironmentSettings> _specialEnvironments;

        private EnvironmentGenerations _environmentState;
        private int _environmentLength;
        private ChanceSystem<int> _generationWeights;
        private int _environmentIndex;


        public int BlockSide = 150; //Сколько метров сторона одного блока

        [SerializeField] private Vector3 StartBlockOffset;  //Позиция стартового блока

        public GeneratorState State;

        [SerializeField] private List<Block> _blockPool;

        [SerializeField] private Transform _player;

        //Длины областей генерации препятствий

        public int StandTricksPerBuilding = 1;
        public int BonusPerBuilding = 1;

        public float ObstacleGenerationWidth = 50f;

        public Block CenterBlock;

        //private ResourcesManager _resourcesManager;
        private List<GameObject> _blockPrefabs;
        private List<GameObject> _challengeBlocks;
        private List<GameObject> _relaxBlocks;

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

            _generationWeights = new ChanceSystem<int>();

            _generationWeights.Add(DEFAULT_INDEX, _defaultEnvironment.nextWeight);
            for (int i = 0; i < _specialEnvironments.Count; i++)
            {
                _generationWeights.Add(i + 1, _specialEnvironments[i].nextWeight);      // 0 занят под дефолтные блоки, так что индекс на 1 больше
            }

            _generationWeights.CalculateChanceWeights();

            _environmentState = EnvironmentGenerations.Default;
            _environmentLength = Mathf.Clamp(_defaultEnvironment.startCount, 0, Mathf.Abs(_defaultEnvironment.startCount));
            _environmentIndex = DEFAULT_INDEX;

            StartCoroutine(Generate());
        }
        
        private void LoadPrefabs()
        {
            //load resources
            _defaultEnvironment = ResourcesManager.DefaultEnvironment;
            _specialEnvironments = ResourcesManager.SpecialEnvironments;

            _blockPrefabs = ResourcesManager.BlockPrefabs;
            _challengeBlocks = _blockPrefabs.FindAll(x => x.GetComponent<Block>().Type == Block.BlockType.Challenge);
            _relaxBlocks = _blockPrefabs.FindAll(x => x.GetComponent<Block>().Type == Block.BlockType.Relax);
        }
        
        private IEnumerator Generate()
        {
            GenerateStartBlock();
            while (true)
            {
                Tick();
                CheckStates();
                yield return new WaitForSeconds(1f);
            }
        }

        private void CheckStates()
        {
            if (State == GeneratorState.Challenge)
            {
                State = GeneratorState.Chill;
            }
            else //Relax 
            {
                State = GeneratorState.Challenge;
            }
        }

        private void GenerateStartBlock()
        {
            var startBlock = (_defaultEnvironment.startPoint == null) ? _defaultEnvironment.blocks.Find(x => x.Type == Block.BlockType.Start) : _defaultEnvironment.startPoint;
            var startBlockGo = Instantiate(startBlock, _player.position + StartBlockOffset, Quaternion.identity);

            _environmentLength--;
                        
            _blockPool.Add(startBlockGo);
            CenterBlock = startBlockGo;
            State = GeneratorState.Challenge;

            GenerateBlocksAfter(startBlockGo);
        }

        private void Tick()
        {
            var newCenterBlock = NewCenterBlock();

            //if (CenterBlock == newCenterBlock)
            //    return;

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
        
        public void GenerateBlocksAfter(Block block)
        {
            var playerXpos = _player.transform.position;
            playerXpos.x = 0;

            var nextXpos = block.transform.position;
            nextXpos.x = 0;
            nextXpos.z += BlockSide;
            playerXpos.y = nextXpos.y;

            //Debug.DrawRay(nextXpos, Vector3.up * 20f, Color.red, 1f);
            //Debug.DrawRay(playerXpos, Vector3.up * 20f, Color.red, 1f);

            if (Vector3.Distance(playerXpos, nextXpos) > GenerateBlocksForward * BlockSide)
            {
                //Debug.DrawLine(playerXpos + Vector3.up * 20f, nextXpos + Vector3.up * 20f, Color.red, 1f);

                return;
            }
            else
            {
                //Debug.DrawLine(playerXpos + Vector3.up * 20f, nextXpos + Vector3.up * 20f, Color.green, 1f);
            }

            Block nextBlockScript;
            if (block.Next == null)
            {
                var nextBlockPrefab = GetRandomBlock();
                var nextBlockGo = Instantiate(nextBlockPrefab,
                    block.transform.position + block.transform.forward * BlockSide, block.transform.rotation);
                nextBlockScript = nextBlockGo.GetComponent<Block>();
                _blockPool.Add(nextBlockScript);
                block.Next = nextBlockScript;
                nextBlockScript.Generate();
                //GenerateBlocksAfter(nextBlockScript);
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
            GameObject result = null;

            switch(_environmentState)
            {
                case EnvironmentGenerations.Default:
                    result = DefaultBlockGeneration();
                    break;

                case EnvironmentGenerations.Weight:
                    result = WeightBlockGeneration();
                    break;

                case EnvironmentGenerations.Special:
                    result = SpecialBlockGeneration();
                    break;
            }

            /*
            if (State == GeneratorState.Chill && _relaxBlocks.Count > 0)
                return _relaxBlocks[Random.Range(0, _relaxBlocks.Count)];
            else
                return _challengeBlocks[Random.Range(0, _challengeBlocks.Count)];
            */

            return result;
        }

        private GameObject DefaultBlockGeneration()
        {
            GameObject result;

            if (_environmentLength > 0)     // Генерируем дефолтные блоки
            {
                result = _defaultEnvironment.blocks[Random.Range(0, _defaultEnvironment.blocks.Count)].gameObject;
                _environmentLength--;
            }
            else        // Запас дефолтных блоков закончился, генерация по весам (дефолтные, или с маленькой вероятностью переходим на спец. блоки)
            {
                _environmentState = EnvironmentGenerations.Weight;
                _environmentLength = 0;

                result = WeightBlockGeneration();
            }

            AudioManager.Instance.PlaySound(Sounds.Bonus);

            return result;
        }

        private GameObject WeightBlockGeneration()
        {
            GameObject result;

            _environmentIndex = _generationWeights.Get();

            if (_environmentIndex == DEFAULT_INDEX)
            {
                result = _defaultEnvironment.blocks[Random.Range(0, _defaultEnvironment.blocks.Count)].gameObject;
            }
            else
            {
                var environment = _specialEnvironments[_environmentIndex - 1];      // 0 индекс под дефолтные блоки, так что спец. блоки на 1 меньше

                _environmentState = EnvironmentGenerations.Special;
                _environmentLength = Random.Range(environment.minCount, environment.maxCount);

                result = (environment.startPoint != null) ? environment.startPoint.gameObject : environment.blocks[Random.Range(0, environment.blocks.Count)].gameObject;

                if (environment.startPoint == null)
                {
                    _environmentLength--;
                }
            }

            AudioManager.Instance.PlaySound(Sounds.Bonus);

            return result;
        }

        private GameObject SpecialBlockGeneration()
        {
            GameObject result;
                        
            var environment = _specialEnvironments[_environmentIndex - 1];      // 0 индекс под дефолтные блоки, так что спец. блоки на 1 меньше

            if (_environmentLength > 0)
            {
                result = environment.blocks[Random.Range(0, environment.blocks.Count)].gameObject;
                _environmentLength--;
            }
            else
            {
                _environmentState = EnvironmentGenerations.Default;
                _environmentLength = _defaultEnvironment.separateCount;
                                
                result = (environment.finishPoint != null) ? environment.finishPoint.gameObject : (_defaultEnvironment.blocks[Random.Range(0, _defaultEnvironment.blocks.Count)].gameObject);

                if (environment.finishPoint == null)
                {
                    _environmentLength--;
                }
            }

            AudioManager.Instance.PlaySound(Sounds.Bonus);

            return result;
        }
    }
}