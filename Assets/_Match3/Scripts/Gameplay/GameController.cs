using Sirenix.OdinInspector;
using Ultimate.Core.Runtime.Singleton;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public BoardController BoardController;

    public override void Init()
    {
    }

    [Button(ButtonSizes.Gigantic)]
    public void Play()
    {
        BoardController.SpawnBoard();
    }

    public Vector2Int WorldPositionToGridPosition(Vector2 worldPostion)
    {
        var checkPoint = GetCheckPoint();

        return Vector2Int.FloorToInt(worldPostion - checkPoint);
    }

    public Vector3 GridPositionToWorldPosition(Vector2Int gridPos)
    {
        var checkPoint = GetCheckPoint();
        return checkPoint + new Vector2(0.5f, 0.5f) + gridPos;
    }

    private Vector2 GetCheckPoint()
    {
        var boardSize = GameManager.Instance.GameConfig.BoardSize;
        var checkPoint = Vector2.zero;
        checkPoint.x = -boardSize.x / 2f;
        checkPoint.y = -boardSize.y / 2f;
        return checkPoint;
    }
}