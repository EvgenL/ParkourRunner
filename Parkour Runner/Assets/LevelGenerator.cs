using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Player.InvectorMods;
using Invector.CharacterController;
using UnityEngine;

enum GeneratorState
{
    HeatUp, //Разогрев
    Callibration, //Каллибровка
    Reward, //Вознаграждение
    Challenge,  //Трудности
    Relax   //Отдых
}

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance;
    [SerializeField] private int BlockSide; //Сколько метров сторона одного блока
    [SerializeField] private float DistanceToGenerate; //Сколько метров от игрока до блока нужно чтобы создать там блок
    [SerializeField] private float DistanceToDestroy; //Сколько метров от игрока до блока нужно чтобы уничтожить пройденный блок

    [SerializeField] private Vector3 StartBlockOffset;  //Позиция стартового блока

    [SerializeField] private string _blockPrefabsPath = "Blocks";
    [SerializeField] private List<GameObject> _blockPrefabs;
    [SerializeField] private GeneratorState _state;

    [SerializeField] private List<Block> _blockPool;

    [SerializeField] private Transform _player;

    void Awake()
    {
        _blockPrefabs = Resources.LoadAll<GameObject>(_blockPrefabsPath + "/").ToList();
    }

    void Start ()
    {
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
                _state = GeneratorState.Callibration;
            }
            else
            {
                GenerateBlocks();
               // DestroyOldBlocks();
            }


           // yield return new WaitForSeconds(1f);
            yield return new WaitForSeconds(0.1f);
        }
    }


    private void GenerateStartBlock()
    {
        var startBlock = _blockPrefabs.Find(x => x.GetComponent<Block>().Type == Block.BlockType.Start);
        var startBlockGo = Instantiate(startBlock, _player.position + StartBlockOffset, Quaternion.identity);
        var startBlockScript = startBlockGo.GetComponent<Block>();
        _blockPool.Add(startBlockScript);
        //_blockPrefabs.Remove(startBlock);
    }

    private void GenerateBlocks()
    {
        var newPool = new List<Block>();
        var lastBlocks = _blockPool.FindAll(x => x.Next == null);
        foreach (Block block in lastBlocks)
        {
            Vector3 playerY0Pos = _player.position;
            playerY0Pos.y = StartBlockOffset.y;

            Vector3 possibleBlockPos;
            Block newBlock;

            if (block.Next == null)
            {
                possibleBlockPos = block.transform.position + block.transform.forward * BlockSide; //Возможная позиция создания следующего блока
                if (Vector3.Distance(playerY0Pos, possibleBlockPos) < DistanceToGenerate)
                {
                    newBlock = GenerateNextBlocks(possibleBlockPos);
                    newBlock.Prev = block;
                    block.Next = newBlock;
                    //Проверяем, не стоял ли слева/справа нового блока другой блок
                    CheckNeighbourBlocks(newBlock);
                    newPool.Add(newBlock);
                    continue;
                }
            }
            if (block.Right == null)
            {
                possibleBlockPos = block.transform.position + block.transform.right * BlockSide; //Возможная позиция создания следующего блока
                if (Vector3.Distance(playerY0Pos, possibleBlockPos) < DistanceToGenerate)
                {
                    newBlock = GenerateNextBlocks(possibleBlockPos);
                    newBlock.Left = block;
                    block.Right = newBlock;

                    //Проверяем, не стоял ли сзади нового блока другой блок
                    CheckNeighbourBlocks(newBlock);
                    newPool.Add(newBlock);
                    continue;

                }
            }
            if (block.Left == null)
            {
                possibleBlockPos = block.transform.position + -block.transform.right * BlockSide; //Возможная позиция создания следующего блока
                if (Vector3.Distance(playerY0Pos, possibleBlockPos) < DistanceToGenerate)
                {
                    newBlock = GenerateNextBlocks(possibleBlockPos);
                    newBlock.Right = block;
                    block.Left = newBlock;

                    //Проверяем, не стоял ли сзади нового блока другой блок
                    CheckNeighbourBlocks(newBlock);
                    newPool.Add(newBlock);
                    continue;
                }
            }

        }
        if (newPool.Count > 0)
        {
            _blockPool.AddRange(newPool);
            GenerateBlocks();
        }
    }

    private void DestroyOldBlocks()
    {
        float zposToDestroy = _player.transform.position.z - DistanceToDestroy;

        //Находим все блоки, которые а) не имеют блоков позади себя, б) расположенны за полем зрения игрока
        var blocksToDestroy = _blockPool.FindAll(x => x.Prev == null && x.transform.position.z <= zposToDestroy).ToList();

        foreach (var block in blocksToDestroy)
        {
            _blockPool.Remove(block);

            //Для соседей удаляем референсы на этот блок
            if (block.Next != null)
                block.Next.Prev = null;
            if (block.Right != null)
                block.Right.Left = null;
            if (block.Left != null)
                block.Left.Right = null;

                Destroy(block);
        }
    }

    private void CheckNeighbourBlocks(Block newBlock)
    {
        var prevBlockPos =
            newBlock.transform.position + -newBlock.transform.forward * BlockSide;
        var leftBlockPos =
            newBlock.transform.position + -newBlock.transform.right * BlockSide;
        var rightBlockPos =
            newBlock.transform.position + newBlock.transform.right * BlockSide;


        foreach (var block in _blockPool)
        {
            if (newBlock == block) continue;
            if (newBlock.Prev == null && prevBlockPos == block.transform.position)
            {
                newBlock.Next = block;
                block.Prev = newBlock;
            }
            else if (newBlock.Left == null && leftBlockPos == block.transform.position)
            {
                newBlock.Left = block;
                block.Right = newBlock;
            }
            else if (newBlock.Right == null && rightBlockPos == block.transform.position)
            {
                newBlock.Right = block;
                block.Left = newBlock;
            }
        }
    }

    public GameObject GetRandomBlock()
    {
        return _blockPrefabs[Random.Range(0, _blockPrefabs.Count)];
    }

    public Block GenerateNextBlocks(Vector3 pos)
    {
        var randBlock = GetRandomBlock();
        var randBlockGo = Instantiate(randBlock, pos, Quaternion.identity);
        var randBlockScript = randBlockGo.GetComponent<Block>();
        return randBlockScript;
    }
}
