using System.Collections.Generic;
using QuickEngine.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    private Dictionary<Vector2Int, Tile> _board = new();

    private void Start()
    {
        Generate();
    }

    [Button(ButtonSizes.Gigantic)]
    public void Generate()
    {
        if (!_board.IsNullOrEmpty())
        {
            DestroyAllTiles();
        }

        var boardSize = GameManager.Instance.GameConfig.BoardSize;
        var currentPosition = Vector2.zero;
        currentPosition.x = -boardSize.x / 2f;
        currentPosition.y = -boardSize.y / 2f;
        var currentIndex = Vector2Int.zero;

        for (var y = 0; y < boardSize.y; y++)
        {
            currentPosition.x = -boardSize.x / 2f;
            for (var x = 0; x < boardSize.x; x++)
            {
                PreventLineMatch(out var ignores, x, currentIndex, y);

                var tile = GameManager.Instance.ObjectPooler.InstantiateRandomTile(ignores);
                tile.transform.position = currentPosition;

                _board.Add(new Vector2Int(x, y), tile);
                currentPosition.x++;
            }

            currentPosition.y++;
        }
    }

    private void PreventLineMatch(out int[] ignores, int x, Vector2Int currentIndex, int y)
    {
        ignores = new int[2];
        for (var i = 0; i < ignores.Length; i++)
        {
            ignores[i] = -1;
        }

        var ignoreIndex = 0;
        if (x >= 2)
        {
            currentIndex.x = x - 1;
            currentIndex.y = y;
            var id = _board[currentIndex].ID;
            currentIndex.x--;
            if (id == _board[currentIndex].ID)
            {
                ignores[ignoreIndex] = id;
                ignoreIndex++;
            }
        }

        if (y >= 2)
        {
            currentIndex.x = x;
            currentIndex.y = y - 1;
            var id = _board[currentIndex].ID;
            currentIndex.y--;
            if (id == _board[currentIndex].ID)
            {
                ignores[ignoreIndex] = id;
            }
        }
    }

    private void DestroyAllTiles()
    {
        foreach (var tile in _board)
        {
            tile.Value?.Destroy();
        }

        _board.Clear();
    }
}