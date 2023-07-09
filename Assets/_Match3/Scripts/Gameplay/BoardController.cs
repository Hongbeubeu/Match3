using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] private BoardGenerator _generator;
    private readonly Dictionary<Vector2Int, Cell> _boardCells = new();

    private void Awake()
    {
        _generator.SetInfo(this);
    }

    public void SpawnBoard()
    {
        DestroyAllTiles();
        InitBoardCell();
        _generator.Generate();
    }

    private void InitBoardCell()
    {
        var boardSize = GameManager.Instance.GameConfig.BoardSize;
        for (var y = 0; y < boardSize.y; y++)
        {
            for (var x = 0; x < boardSize.x; x++)
            {
                _boardCells[new Vector2Int(x, y)] = new Cell();
            }
        }
    }

    public Tile GetTile(Vector2Int pos)
    {
        return _boardCells[pos].Tile;
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
}