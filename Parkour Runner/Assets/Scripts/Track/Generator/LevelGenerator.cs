using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ParkourRunner.Scripts.Player.InvectorMods;
using UnityEngine;
using AEngine;

namespace ParkourRunner.Scripts.Track.Generator
{
    public class LevelGenerator : MonoBehaviour
    {
        private const int DEFAULT_INDEX = 0;
        
        private enum EnvironmentGenerations
        {
            Default,        // Дефолтные блоки в определенном количестве
            Weight,         // Генерация дефолтных блоков (наибольший вес), пока не сработает вес специальных блоков
            Special,        // Специальные блоки в определенном количестве (Robopolis, Tunnel)
            Level,          // Генерация ограниченного числа блоков в уровне
            Tutorial        // Генерация уровня-обучения. Генерация идет подряд а не случайным образом.
        }

        #region Singleton
        public static LevelGenerator Instance;
        #endregion
        
        [SerializeField] private EnvironmentController _environmentController;
        [SerializeField] private float _blockSize = 150;
        [SerializeField] private int _generateBlocksForward = 2;
        [SerializeField] private int _numberOfNonRepeatingBlocks = 3;
        [SerializeField] private float _levelCaptionDelay = 1f;

        [SerializeField] private Vector3 StartBlockOffset;
        [SerializeField] private List<Block> _blockPool;

        private Environment _environment;
        public Block CenterBlock;

        [Header("Debug mode [key '1']")]
        [SerializeField] private bool _debugMode;
        
        private Transform _player;

        private Environment.DefaulEnvironmentSettings _defaultEnvironment;
        private List<Environment.SpecialEnvironmentSettings> _specialEnvironments;
        private Environment.LevelEnvironmentSettings _levelEnvironment;
        private Environment.TutorialEnvironmentSettings _tutorialEnvironment;
        private List<string> _history;

        private EnvironmentGenerations _generationState;
        private int _environmentLength;
        private ChanceSystem<int> _generationWeights;
        private int _environmentIndex;

        private List<string> _lastBlocks;

        public float BlockSize { get { return _blockSize; } }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _lastBlocks = new List<string>();
                _history = new List<string>();

                _environment = _environmentController.GetActualEnvironment();

                _defaultEnvironment = _environment.DefaultEnvironment;
                _specialEnvironments = _environment.SpecialEnvironments;
                _levelEnvironment = _environment.LevelEnvironment;
                _tutorialEnvironment = _environment.TutorialEnvironment;
            }
            else
            {
                Destroy(this);
                return;
            }
        }

        private void Start()
        {
            int count = (_environment.EndlessLevel) ? _defaultEnvironment.blocks.Count : _levelEnvironment.blocks.Count;

            if (_environment.TutorialLevel)
                count = _tutorialEnvironment.blocks.Count;

            if (count == 0)
            {
                Debug.LogError("Не найдено ни одного блока в настройках уровня");
                Destroy(this);
                return;
            }
            
            _player = ParkourThirdPersonController.instance.transform;
                        
            if (_environment.EndlessLevel)
            {
                _generationWeights = new ChanceSystem<int>();

                _generationWeights.Add(DEFAULT_INDEX, _defaultEnvironment.nextWeight);
                for (int i = 0; i < _specialEnvironments.Count; i++)
                {
                    _generationWeights.Add(i + 1, _specialEnvironments[i].nextWeight);      // 0 занят под дефолтные блоки, так что индекс на 1 больше
                }

                _generationWeights.CalculateChanceWeights();
            }
                        
            _generationState = (_environment.EndlessLevel) ? EnvironmentGenerations.Default : EnvironmentGenerations.Level;

            if (_environment.TutorialLevel)
                _generationState = EnvironmentGenerations.Tutorial;

            int length = _environment.EndlessLevel ? _defaultEnvironment.startCount : _levelEnvironment.blocksCount;

            if (_environment.TutorialLevel)
                length = count;

            _environmentLength = Mathf.Clamp(length, 0, Mathf.Abs(length));
            _environmentIndex = DEFAULT_INDEX;

            StartCoroutine(Generate());
            StartCoroutine(ShowLevelDataProcess(_levelCaptionDelay));
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

        private IEnumerator ShowLevelDataProcess(float delay)
        {
            yield return new WaitForSeconds(delay);

            Managers.HUDManager.Instance.ShowGreatMessage(Managers.HUDManager.Messages.CurrentLevel);
        }
                        
        private IEnumerator Generate()
        {
            GenerateStartBlock();

            while (true)
            {
                Tick();
                yield return new WaitForSeconds(1f);
            }
        }
                
        private void GenerateStartBlock()
        {
            Block defaultBlock = null;

            if (_environment.TutorialLevel)
                defaultBlock = _tutorialEnvironment.blocks.Count > 0 ? _tutorialEnvironment.blocks[0] : null;
            else
                defaultBlock = (_environment.EndlessLevel) ? _defaultEnvironment.startPoint : _levelEnvironment.start;

            var startList = (_environment.EndlessLevel || _environment.TutorialLevel) ? _defaultEnvironment.blocks : _levelEnvironment.blocks;

            Block startBlock = (defaultBlock == null) ? startList.Find(x => x.Type == Block.BlockType.Start) : defaultBlock;
                        
            var startBlockGo = Instantiate(startBlock, _player.position + StartBlockOffset, Quaternion.identity);

            _environmentLength--;
                        
            _blockPool.Add(startBlockGo);
            CenterBlock = startBlockGo;
            
            _lastBlocks.Add(startBlockGo.name);
            if (_debugMode)
                _history.Add(startBlockGo.name);
            
            GenerateBlocksAfter(startBlockGo);
        }

        private void Tick()
        {
            var newCenterBlock = NewCenterBlock();

            GenerateBlocksAfter(newCenterBlock);
            DestroyOldBlocks();
            CenterBlock = newCenterBlock;
        }
                
        private Block NewCenterBlock()
        {
            Block newCenterBlock = _blockPool[0];
            foreach (var block in _blockPool)
            {
                Vector3 blockPos = block.transform.position;

                Vector3 playerPos = _player.position;
                if (playerPos.z > blockPos.z - _blockSize / 2f && playerPos.z <= blockPos.z + _blockSize / 2f)
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
            nextXpos.z += _blockSize;
            playerXpos.y = nextXpos.y;
            
            if (Vector3.Distance(playerXpos, nextXpos) > _generateBlocksForward * _blockSize)
                return;
            
            if (block.Next == null)
            {
                var nextBlock = Instantiate(GetNextBlock(), block.transform.position + block.transform.forward * _blockSize, block.transform.rotation);

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
                if (block.transform.position.z < _player.position.z - (_blockSize/2f + 2f)) //Если игрок сощёл с предыдущего блока на 2 метра
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
        
        public Block GetNextBlock()
        {
            Block result = null;

            switch(_generationState)
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

                case EnvironmentGenerations.Level:
                    result = LevelBlockGeneration();
                    break;

                case EnvironmentGenerations.Tutorial:
                    result = TutorialBlockGeneration();
                    break;
            }
                        
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
                _generationState = EnvironmentGenerations.Weight;
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

                _generationState = EnvironmentGenerations.Special;
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
                _generationState = EnvironmentGenerations.Default;
                _environmentLength = _defaultEnvironment.separateCount;
                                
                result = (environment.finishPoint != null) ? environment.finishPoint : GetBlockFromList(_defaultEnvironment.blocks);

                if (environment.finishPoint == null)
                {
                    _environmentLength--;
                }
            }

            return result;
        }

        private Block TutorialBlockGeneration()
        {
            Block result = null;

            if (_environmentLength > 0)
            {
                int index = _tutorialEnvironment.blocks.Count - _environmentLength;
                result = _tutorialEnvironment.blocks[index];
                _environmentLength--;
            }
            else
            {
                result = _tutorialEnvironment.blocks[_tutorialEnvironment.blocks.Count - 1];
            }

            return result;
        }

        private Block LevelBlockGeneration()
        {
            Block result = null;

            if (_environmentLength > 0)
            {
                result = GetBlockFromList(_levelEnvironment.blocks);
                _environmentLength--;
            }
            else
            {
                result = _levelEnvironment.finish;
            }
            
            return result;
        }
    }
}