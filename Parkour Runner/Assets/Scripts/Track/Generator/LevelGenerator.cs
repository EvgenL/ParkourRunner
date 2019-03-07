using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            Challenge,
            Chill
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

        private List<string> _lastBlocks;
        
        public int BlockSide = 150; //Сколько метров сторона одного блока

        [SerializeField] private Vector3 StartBlockOffset;  //Позиция стартового блока

        public GeneratorState State;

        [SerializeField] private List<Block> _blockPool;

        [SerializeField] private int _numberOfNonRepeatingBlocks = 3;

        [SerializeField] private Transform _player;

        //Длины областей генерации препятствий

        public int StandTricksPerBuilding = 1;
        public int BonusPerBuilding = 1;

        public float ObstacleGenerationWidth = 50f;

        public Block CenterBlock;

        [SerializeField] private bool _debugMode;

        private List<string> _history;

        //private List<GameObject> _blockPrefabs;
        //private List<GameObject> _challengeBlocks;
        //private List<GameObject> _relaxBlocks;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _lastBlocks = new List<string>();
                _history = new List<string>();
            }
            else
            {
                Destroy(this);
                return;
            }

            LoadPrefabs();
        }

        private void Start()
        {
            if (_defaultEnvironment.blocks.Count == 0)
            {
                Debug.LogError("Не найдено ни одного блока в папке Resources/Blocks");
                Destroy(this);
                return;
            }
            
            _player = ParkourThirdPersonController.instance.transform;
                                    
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

        private void Update()
        {
            if (_debugMode && Input.GetKeyUp(KeyCode.Alpha1))
            {
                print(string.Format("Generated {0} blocks", _history.Count));
                foreach (var block in _history)
                {
                    print(block);
                }

                Debug.Break();
            }
        }

        private void LoadPrefabs()
        {
            _defaultEnvironment = ResourcesManager.DefaultEnvironment;
            _specialEnvironments = ResourcesManager.SpecialEnvironments;

            //_blockPrefabs = ResourcesManager.BlockPrefabs;
            //_challengeBlocks = _blockPrefabs.FindAll(x => x.GetComponent<Block>().Type == Block.BlockType.Challenge);
            //_relaxBlocks = _blockPrefabs.FindAll(x => x.GetComponent<Block>().Type == Block.BlockType.Relax);
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

            _lastBlocks.Add(startBlockGo.name);
            if (_debugMode)
                _history.Add(startBlockGo.name);
            
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

            if (block.Next == null)
            {
                var nextBlock = Instantiate(GetRandomBlock(), block.transform.position + block.transform.forward * BlockSide, block.transform.rotation);

                _blockPool.Add(nextBlock);
                block.Next = nextBlock;
                nextBlock.Generate();
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
        
        public Block GetRandomBlock()
        {
            Block result = null;

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

            _lastBlocks.Add(result.name);
            if (_debugMode)
                _history.Add(result.name);
                        
            if (_lastBlocks.Count > _numberOfNonRepeatingBlocks)
            {
                _lastBlocks.RemoveAt(0);
            }

            return result;
        }

        private Block GetBlockFromList(List<Block> list)
        {
            var blocks = new List<Block>(list);
            foreach (var blockName in _lastBlocks)
            {
                blocks = blocks.Where(x => x.name != blockName).ToList();
            }
            
            return blocks[Random.Range(0, blocks.Count)];
        }

        private Block DefaultBlockGeneration()
        {
            Block result;

            if (_environmentLength > 0)     // Генерируем дефолтные блоки
            {
                result = GetBlockFromList(_defaultEnvironment.blocks);
                _environmentLength--;
            }
            else        // Запас дефолтных блоков закончился, генерация по весам (дефолтные, или с маленькой вероятностью переходим на спец. блоки)
            {
                _environmentState = EnvironmentGenerations.Weight;
                _environmentLength = 0;

                result = WeightBlockGeneration();
            }

            return result;
        }

        private Block WeightBlockGeneration()
        {
            Block result;

            _environmentIndex = _generationWeights.Get();

            if (_environmentIndex == DEFAULT_INDEX)
            {
                result = GetBlockFromList(_defaultEnvironment.blocks);
            }
            else
            {
                var environment = _specialEnvironments[_environmentIndex - 1];      // 0 индекс под дефолтные блоки, так что спец. блоки на 1 меньше

                _environmentState = EnvironmentGenerations.Special;
                _environmentLength = Random.Range(environment.minCount, environment.maxCount);

                result = (environment.startPoint != null) ? environment.startPoint : GetBlockFromList(environment.blocks);

                if (environment.startPoint == null)
                {
                    _environmentLength--;
                }
            }

            return result;
        }

        private Block SpecialBlockGeneration()
        {
            Block result;
                        
            var environment = _specialEnvironments[_environmentIndex - 1];      // 0 индекс под дефолтные блоки, так что спец. блоки на 1 меньше

            if (_environmentLength > 0)
            {
                result = GetBlockFromList(environment.blocks);
                _environmentLength--;
            }
            else
            {
                _environmentState = EnvironmentGenerations.Default;
                _environmentLength = _defaultEnvironment.separateCount;
                                
                result = (environment.finishPoint != null) ? environment.finishPoint : GetBlockFromList(_defaultEnvironment.blocks);

                if (environment.finishPoint == null)
                {
                    _environmentLength--;
                }
            }

            return result;
        }
    }
}