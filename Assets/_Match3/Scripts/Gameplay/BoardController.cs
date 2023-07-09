using System.Collections.Generic;
using System.Linq;
using QuickEngine.Extensions;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] private BoardGenerator _generator;
    private readonly Dictionary<Vector2Int, Cell> _boardCells = new();
    private Vector2Int _boardSize;

    private void Awake()
    {
        _generator.SetInfo(this);
    }

    public void SpawnBoard()
    {
        _boardSize = GameManager.Instance.GameConfig.BoardSize;
        DestroyAllTiles();
        InitBoardCell();
        _generator.Generate();
    }

    private void InitBoardCell()
    {
        for (var y = 0; y < _boardSize.y; y++)
        {
            for (var x = 0; x < _boardSize.x; x++)
            {
                _boardCells[new Vector2Int(x, y)] = new Cell();
            }
        }
    }

    public Tile GetTile(Vector2Int pos)
    {
        return _boardCells.IsNullOrEmpty() ? null : _boardCells[pos]?.Tile;
    }

    public Cell GetCell(Vector2Int pos)
    {
        return _boardCells[pos];
    }

    public void AddTile(Vector2Int pos, Tile tile)
    {
        _boardCells[pos].SetInfo(pos, tile);
    }

    private void DestroyAllTiles()
    {
        foreach (var cell in _boardCells)
        {
            cell.Value?.DestroyTile();
        }

        _boardCells.Clear();
    }

    public void SwapTile(Vector2Int tilePos1, Vector2Int tilePos2)
    {
        GameController.Instance.SetHoldedCell(null);
        var dir = tilePos2 - tilePos1;
        if (Mathf.Abs(dir.x) == 1 && dir.y == 0 || dir.x == 0 && Mathf.Abs(dir.y) == 1)
        {
            var temp = _boardCells[tilePos1].Tile;
            _boardCells[tilePos1].Tile = _boardCells[tilePos2].Tile;
            _boardCells[tilePos2].Tile = temp;
            _boardCells[tilePos1].UpdatePosition();
            _boardCells[tilePos2].UpdatePosition();
            CheckMatched(tilePos1);
            CheckMatched(tilePos2);
        }
    }

    private readonly List<Cell> CellMatcheds = new();

    private void CheckMatched(Vector2Int tilePos)
    {
        CellMatcheds.Clear();

        var id = _boardCells[tilePos].Tile.ID;
        var lineCells = new List<Cell> {GetCell(tilePos)};
        //Check line
        if (tilePos.x > 0 && GetTile(tilePos + Vector2Int.left) != null && GetTile(tilePos + Vector2Int.left).ID == id)
        {
            lineCells.Add(GetCell(tilePos + Vector2Int.left));
            if (tilePos.x > 1 && GetTile(tilePos + Vector2Int.left * 2) != null &&
                GetTile(tilePos + Vector2Int.left * 2).ID == id)
            {
                lineCells.Add(GetCell(tilePos + Vector2Int.left * 2));
            }
        }

        if (tilePos.x < _boardSize.x - 1 && GetTile(tilePos + Vector2Int.right) != null &&
            GetTile(tilePos + Vector2Int.right).ID == id)
        {
            lineCells.Add(GetCell(tilePos + Vector2Int.right));
            if (tilePos.x < _boardSize.x - 2 && GetTile(tilePos + Vector2Int.right * 2) != null &&
                GetTile(tilePos + Vector2Int.right * 2).ID == id)
            {
                lineCells.Add(GetCell(tilePos + Vector2Int.right * 2));
            }
        }

        if (lineCells.Count < 3)
        {
            lineCells.Clear();
        }
        else
        {
            CellMatcheds.AddRange(lineCells);
            lineCells.Clear();
        }

        //Check column
        lineCells.Add(GetCell(tilePos));

        if (tilePos.y > 0 && GetTile(tilePos + Vector2Int.down) != null &&
            GetTile(tilePos + Vector2Int.down).ID == id)
        {
            lineCells.Add(GetCell(tilePos + Vector2Int.down));
            if (tilePos.y > 1 && GetTile(tilePos + Vector2Int.down * 2) != null &&
                GetTile(tilePos + Vector2Int.down * 2).ID == id)
            {
                lineCells.Add(GetCell(tilePos + Vector2Int.down * 2));
            }
        }

        if (tilePos.y < _boardSize.y - 1 && GetTile(tilePos + Vector2Int.up) != null &&
            GetTile(tilePos + Vector2Int.up).ID == id)
        {
            lineCells.Add(GetCell(tilePos + Vector2Int.up));
            if (tilePos.y < _boardSize.y - 2 && GetTile(tilePos + Vector2Int.up * 2) &&
                GetTile(tilePos + Vector2Int.up * 2).ID == id)
            {
                lineCells.Add(GetCell(tilePos + Vector2Int.up * 2));
            }
        }

        if (lineCells.Count < 3)
        {
            lineCells.Clear();
        }
        else
        {
            CellMatcheds.AddRange(lineCells);
            lineCells.Clear();
        }

        foreach (var cell in CellMatcheds)
        {
            cell.Tile?.Destroy();
            cell.Tile = null;
        }
    }
    
    public bool IsInsideBoard(Vector2Int gridPos)
    {
        return gridPos.x >= 0 && gridPos.x < _boardSize.x && gridPos.y >= 0 && gridPos.y < _boardSize.y;
    }
}