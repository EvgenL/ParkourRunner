using System.Collections;
using System.Collections.Generic;
using Basic_Locomotion.Scripts.CharacterController;
using ParkourRunner.Scripts.Managers;
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
            }
        }

        #endregion

        public enum GeneratorState
        {
            HeatUp, //Разогрев
            Callibration, //Каллибровка
            Reward, //Вознаграждение
            Challenge, //Трудности
            Relax //Отдых
        }

        //TODO для всей метровки

        public float StateLength
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

        [SerializeField] private int BlockSide; //Сколько метров сторона одного блока

        [SerializeField] private Vector3 StartBlockOffset;  //Позиция стартового блока

        public GeneratorState State;

        [SerializeField] private List<Block> _blockPool;

        [SerializeField] private Transform _player;

        //Длины областей генерации препятствий
        [SerializeField] private float HeatUpStateLength = 20;
        [SerializeField] private float CallibrationStateLength = 20;
        [SerializeField] private float RewardStateLength = 15;
        [SerializeField] private float ChallengeStateLength = 25;
        [SerializeField] private float RelaxStateLength = 15;

        public int StandTricksPerBuilding = 1;
        public int BonusPerBuilding = 1;

        [SerializeField] private float ObstacleGenerationdistance = 100f;
        public float ObstacleGenerationWidth = 50f;

        private Block _oldCenter;

        private ResourcesManager _resourcesManager;
        private List<GameObject> _blockPrefabs;

        void Start ()
        {
            _resourcesManager = ResourcesManager.Instance;
            _blockPrefabs = _resourcesManager.BlockPrefabs;
            transform.position = _player.position + StartBlockOffset;
            transform.position = _player.position + StartBlockOffset;

            if (_player == null)
            {
                _player = FindObjectOfType<vThirdPersonController>().transform;
            }
            //_player = vThirdPersonController.instance.transform;
        
            State = GeneratorState.HeatUp;
            StartCoroutine(Generate());
            Instance = this;

        }

        private IEnumerator Generate()
        {
            while (true)
            {
                if (State == GeneratorState.HeatUp)
                {
                    GenerateStartBlock();
                    State = GeneratorState.Challenge; //tODO
                }
                GenerateBlocks();
                GenerateObstacles();


                // yield return new WaitForSeconds(1f);
                yield return new WaitForSeconds(1f);
            }
        }

        private void GenerateObstacles()
        {
            while (transform.position.z < _player.position.z + ObstacleGenerationdistance)
            {
                GenerateObstaclesSegment();
            }
        }


        private void GenerateStartBlock()
        {
            var startBlock = _blockPrefabs.Find(x => x.GetComponent<Block>().Type == Block.BlockType.Start);
            var startBlockGo = Instantiate(startBlock, _player.position + StartBlockOffset, Quaternion.identity);
            var startBlockScript = startBlockGo.GetComponent<Block>();
            _blockPool.Add(startBlockScript);
            _blockPrefabs.Remove(startBlock);
        }

        private void GenerateBlocks()
        {
            var newPool = new List<Block>();

            //Выбираем блоки, которые стоят по краям карты


            Block centerBlock = _blockPool[0];
            foreach (var block in _blockPool)
            {
                Vector3 blockPos = block.transform.position;

                Vector3 playerPos = _player.position;
                playerPos.y = StartBlockOffset.y;

                if (playerPos.x > blockPos.x - BlockSide / 2f && playerPos.x <= blockPos.x + BlockSide / 2f
                                                              && playerPos.z > blockPos.z - BlockSide / 2f && playerPos.z <= blockPos.z + BlockSide / 2f)
                {
                    centerBlock = block;
                    break;
                }
            }
            ;

            if (_oldCenter == centerBlock)
                return;

            GenerateBlocksAround(centerBlock);
            DestroyOldBlocks();
            _oldCenter = centerBlock;
            GenerateObstaclesOnNewBlocks();
        }

        private void GenerateObstaclesOnNewBlocks()
        {
            GoBack();
            GenerateObstacles();
        }

        private void GoBack()
        {
            while (transform.position.z > _player.position.z)
            {
                switch (State)
                {
                    case GeneratorState.HeatUp:
                        break;

                    case GeneratorState.Callibration:
                        State = GeneratorState.HeatUp;
                        transform.position -= Vector3.forward * HeatUpStateLength;
                        break;

                    case GeneratorState.Reward:
                        State = GeneratorState.Callibration;
                        transform.position -= Vector3.forward * CallibrationStateLength;
                        break;

                    case GeneratorState.Challenge:
                        State = GeneratorState.Relax;
                        transform.position -= Vector3.forward * RelaxStateLength;
                        break;

                    case GeneratorState.Relax:
                        State = GeneratorState.Challenge;
                        transform.position -= Vector3.forward * ChallengeStateLength;
                        break;
                }
            }
        }


        public void GenerateBlocksAround(Block block)
        {
            if (block.Next == null)
            {
                var nextBlockPrefab = GetRandomBlock();
                var nextBlockGo = Instantiate(nextBlockPrefab,
                    block.transform.position + block.transform.forward * BlockSide, block.transform.rotation);
                var nextBlocScript = nextBlockGo.GetComponent<Block>();
                _blockPool.Add(nextBlocScript);
                block.Next = nextBlocScript;
            }

            if (block.Right == null)
            {
                var rightBlockPrefab = GetRandomBlock();
                var rightBlockGo = Instantiate(rightBlockPrefab,
                    block.transform.position + block.transform.right * BlockSide, block.transform.rotation);
                var rightBlocScript = rightBlockGo.GetComponent<Block>();
                _blockPool.Add(rightBlocScript);
                block.Right = rightBlocScript;
                rightBlocScript.Left = block;
            }

            if (block.Left == null)
            {
                var leftBlockPrefab = GetRandomBlock();
                var leftBlockGo = Instantiate(leftBlockPrefab,
                    block.transform.position + -block.transform.right * BlockSide, block.transform.rotation);
                var leftBlocScript = leftBlockGo.GetComponent<Block>();
                _blockPool.Add(leftBlocScript);
                block.Left = leftBlocScript;
                leftBlocScript.Right = block;
            }
        
            if (block.Left.Next == null)
            {
                var lnBlockPrefab = GetRandomBlock();
                var lnBlockGo = Instantiate(lnBlockPrefab, block.transform.position + -block.transform.right * BlockSide +
                                                           block.transform.forward * BlockSide, block.transform.rotation);
                var lnBlocScript = lnBlockGo.GetComponent<Block>();
                _blockPool.Add(lnBlocScript);
                block.Left.Next = lnBlocScript;
                lnBlocScript.Right = block.Next;

                block.Next.Left = lnBlocScript;
            }

            if (block.Right.Next == null)
            {
                var rnBlockPrefab = GetRandomBlock();
                var rnBlockGo = Instantiate(rnBlockPrefab, block.transform.position + block.transform.right * BlockSide +
                                                           block.transform.forward * BlockSide, block.transform.rotation);
                var rnBlocScript = rnBlockGo.GetComponent<Block>();
                _blockPool.Add(rnBlocScript);
                block.Right.Next = rnBlocScript;
                rnBlocScript.Left = block.Next;

                block.Next.Right = rnBlocScript;
            }
        }

        private void DestroyOldBlocks()
        {
            List<Block> blocksToDestroy = new List<Block>();
            foreach (var block in _blockPool)
            {
                if (block.transform.position.z < _player.position.z - (BlockSide/2f + 2f)
                    || (block.transform.position.x < _player.position.x - BlockSide  * 2
                        || block.transform.position.x > _player.position.x + BlockSide * 2))
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

        private void GenerateObstaclesSegment()
        {
            switch (State)
            {
                case GeneratorState.HeatUp:
                    //В разогреве не генерится ничего
                    State = GeneratorState.Callibration;
                    transform.position += Vector3.forward * HeatUpStateLength;
                    break;

                case GeneratorState.Callibration:
                    GenerateCallibration();
                    State = GeneratorState.Reward;
                    transform.position += Vector3.forward * CallibrationStateLength;
                    break;

                case GeneratorState.Reward:
                    GenerateReward();
                    State = GeneratorState.Challenge;
                    transform.position += Vector3.forward * RewardStateLength;
                    break;

                case GeneratorState.Challenge:
                    GenerateChallenge();
                    State = GeneratorState.Relax;
                    transform.position += Vector3.forward * ChallengeStateLength;
                    break;

                case GeneratorState.Relax:
                    GenerateRelax();
                    State = GeneratorState.Challenge;
                    transform.position += Vector3.forward * RelaxStateLength;
                    break;
            }
            //todo pos
        }

        private void GenerateCallibration()
        {
            var blocks = SelectBlocksInRange(transform.position.z, CallibrationStateLength);
            foreach (var block in blocks)
            {
                block.Generate();
            }
        }
        private void GenerateReward()
        {
            GenerateCallibration(); //TODO
        }
        private void GenerateRelax()
        {
            GenerateCallibration(); //TODO
        }
        private void GenerateChallenge()
        {
            GenerateCallibration(); //TODO
        }

        private List<Block> SelectBlocksInRange(float positionZ, float stateLength)
        {
            float z1 = positionZ;
            float z2 = positionZ + stateLength;

            List<Block> blocksRange = new List<Block>();
            foreach (var block in _blockPool)
            {
                if (!(block.transform.position.z + BlockSide < z2 
                      || block.transform.position.z - BlockSide > z1))
                {
                    blocksRange.Add(block);
                }
            }

            return blocksRange;
        }


        public GameObject GetRandomBlock()
        {
            return _blockPrefabs[Random.Range(0, _blockPrefabs.Count)];
        }
    }
    class PossibleBlockPoint
    {
        public Vector3 Position;
        public Block Next;
        public Block Prev;
        public Block Left;
        public Block Right;
    }
}