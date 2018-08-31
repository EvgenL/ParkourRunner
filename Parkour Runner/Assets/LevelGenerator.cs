using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Player.InvectorMods;
using Invector.CharacterController;
using UnityEngine;


public class LevelGenerator : MonoBehaviour
{
    
public enum GeneratorState
{
    HeatUp, //Разогрев
    Callibration, //Каллибровка
    Reward, //Вознаграждение
    Challenge,  //Трудности
    Relax   //Отдых
}

public static LevelGenerator Instance;
    [SerializeField] private int BlockSide; //Сколько метров сторона одного блока

    [SerializeField] private Vector3 StartBlockOffset;  //Позиция стартового блока

    [SerializeField] private string _blockPrefabsPath = "Blocks";
    [SerializeField] private List<GameObject> _blockPrefabs;
    [SerializeField] private LevelGenerator.GeneratorState _state;

    [SerializeField] private List<Block> _blockPool;

    [SerializeField] private Transform _player;

    //Длины областей генерации препятствий
    [SerializeField] private float HeatUpStateLength = 20;
    [SerializeField] private float CallibrationStateLength = 20;
    [SerializeField] private float RewardStateLength = 15;
    [SerializeField] private float ChallengeStateLength = 25;
    [SerializeField] private float RelaxStateLength = 15;

    [SerializeField] private float ObstacleGenerationdistance = 10f;

    private Block _oldCenter;

    void Awake()
    {
        _blockPrefabs = Resources.LoadAll<GameObject>(_blockPrefabsPath + "/").ToList();
    }

    void Start ()
    {
        transform.position = _player.position + StartBlockOffset;

        if (_player == null)
        {
            _player = FindObjectOfType<vThirdPersonController>().transform;
        }
        //_player = vThirdPersonController.instance.transform;
        
        _state = GeneratorState.HeatUp;
	    StartCoroutine(Generate());
        Instance = this;

    }

    private IEnumerator Generate()
    {
        while (true)
        {
            if (_state == GeneratorState.HeatUp)
            {
                GenerateStartBlock();
                _state = GeneratorState.Challenge; //tODO
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
        else
            _oldCenter = centerBlock;

        GenerateBlocksAround(centerBlock);
        //DestroyOldBlocks();
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
                || (block.transform.position.x < _player.position.x - BlockSide 
                    || block.transform.position.x > _player.position.x + BlockSide))
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
        switch (_state)
        {
            case GeneratorState.HeatUp:
                //В разогреве не генерится ничего
                _state = GeneratorState.Callibration;
                transform.position += Vector3.forward * HeatUpStateLength;
                break;

            case GeneratorState.Callibration:
                GenerateCallibration();
                _state = GeneratorState.Reward;
                transform.position += Vector3.forward * CallibrationStateLength;
                break;

            case GeneratorState.Reward:
                GenerateReward();
                _state = GeneratorState.Challenge;
                transform.position += Vector3.forward * RewardStateLength;
                break;

            case GeneratorState.Challenge:
                GenerateChallenge();
                _state = GeneratorState.Relax;
                transform.position += Vector3.forward * ChallengeStateLength;
                break;

            case GeneratorState.Relax:
                GenerateRelax();
                _state = GeneratorState.Challenge;
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
            block.GenerateCallibration(transform.position.z, CallibrationStateLength);
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
