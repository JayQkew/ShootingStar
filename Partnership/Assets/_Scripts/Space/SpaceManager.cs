using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpaceManager : MonoBehaviour
{
    public static SpaceManager Instance { get; private set; }

    public Grid grid;
    public GameObject mainShip;
    public Vector2Int playerCell;
    public Vector2Int[] surroundingCells = new Vector2Int[0];
    public GameObject[] surroundingChunks = new GameObject[9];
    public LayerMask chunkLayer;
    public Transform world;

    public List<GameObject> spaceChunks = new List<GameObject>();
    public Dictionary<Vector2Int, GameObject> cellChunkPairs = new Dictionary<Vector2Int, GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        GetSurroundingCells();
        //CellCheck();
        
    }

    private void GetPlayerCell() => playerCell = (Vector2Int)grid.WorldToCell(mainShip.transform.position);

    private void GetSurroundingCells()
    {
        GetPlayerCell();

        List<Vector2Int> cells = new List<Vector2Int>();

        for (int x = playerCell.x - 1; x < playerCell.x + 2; x++)
        {
            for (int y = playerCell.y + 1; y > playerCell.y - 2; y--)
            {
                cells.Add(new Vector2Int(x, y));
            }
        }

        surroundingCells = cells.ToArray();
    }

    private void CellCheck()
    {
        for (int i = 0; i < surroundingCells.Length - 1; i++)
        {
            if (!ChunkChecker(surroundingCells[i])) // this cell doesnt contains a chunk
            {
                // check if this cell has been stored
                // if no
                if (!cellChunkPairs.ContainsKey(surroundingCells[i]))   
                {
                    //spawn a random chunk 
                    GameObject newChunk = Instantiate(spaceChunks[Random.Range(0, surroundingCells.Length - 1)], grid.GetCellCenterWorld((Vector3Int)surroundingCells[i]), Quaternion.identity, world);
                    //ADD key-value pair to dictionary
                    cellChunkPairs.Add(surroundingCells[i], newChunk);
                    surroundingChunks[i] = newChunk;

                }
                // if yes
                else
                {
                    //spawn assigned value of the key
                    GameObject oldChunk = Instantiate(cellChunkPairs[surroundingCells[i]], grid.GetCellCenterWorld((Vector3Int)surroundingCells[i]), Quaternion.identity, world);
                    surroundingChunks[i] = oldChunk;
                }

            }
            else
            {
                surroundingChunks[i] = ChunkChecker_GO(surroundingCells[i]);
            }

        }

    }

    private bool ChunkChecker(Vector2Int cell)
    {
        Vector2 worldCell = grid.GetCellCenterWorld((Vector3Int)cell);

        RaycastHit2D hit = Physics2D.BoxCast(worldCell, Vector2.one, 0, Vector2.zero, 0, chunkLayer);

        if(hit == true) return true;
        else return false;
    }

    private GameObject ChunkChecker_GO(Vector2Int cell)
    {
        Vector2 worldCell = grid.GetCellCenterWorld((Vector3Int)cell);

        RaycastHit2D hit = Physics2D.BoxCast(worldCell, Vector2.one, 0, Vector2.zero, 0, chunkLayer);

        if (hit == true) return hit.transform.parent.gameObject;
        else return null;
    }
}
