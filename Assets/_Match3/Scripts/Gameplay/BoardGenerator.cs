using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    private BoardController _controller;

    public void SetInfo(BoardController controller)
    {
        _controller = controller;
    }

    public void Generate()
    {
        var boardSize = GameManager.Instance.GameConfig.BoardSize;

        var currentPosition = Vector2.zero;
        currentPosition.x = -boardSize.x / 2f + 0.5f;
        currentPosition.y = -boardSize.y / 2f + 0.5f;
        var currentGridPos = Vector2Int.zero;

        for (var y = 0; y < boardSize.y; y++)
        {
            currentPosition.x = -boardSize.x / 2f + 0.5f;
            for (var x = 0; x < boardSize.x; x++)
            {
                PreventLineMatch(out var ignores, x, currentGridPos, y);

                var tile = GameManager.Instance.ObjectPooler.InstantiateRandomTile(ignores);
                tile.transform.position = currentPosition;
                var pos = new Vector2Int(x, y);
                _controller.AddTile(pos, tile);
                currentPosition.x++;
            }

            currentPosition.y++;
        }
    }

    private void PreventLineMatch(out int[] ignores, int x, Vector2Int currentGridPos, int y)
    {
        ignores = new int[2];
        for (var i = 0; i < ignores.Length; i++)
        {
            ignores[i] = -1;
        }

        var ignoreIndex = 0;

        if (x >= 2)
        {
            currentGridPos.x = x - 1;
            currentGridPos.y = y;
            var id = _controller.GetTile(currentGridPos).ID;
            currentGridPos.x--;
            if (id == _controller.GetTile(currentGridPos).ID)
            {
                ignores[ignoreIndex] = id;
                ignoreIndex++;
            }
        }

        if (y >= 2)
        {
            currentGridPos.x = x;
            currentGridPos.y = y - 1;
            var id = _controller.GetTile(currentGridPos).ID;
            currentGridPos.y--;
            if (id == _controller.GetTile(currentGridPos).ID)
            {
                ignores[ignoreIndex] = id;
            }
        }
    }
}