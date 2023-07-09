using Sirenix.OdinInspector;
using Ultimate.Core.Runtime.Singleton;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private GameObject _selected;

    public BoardController BoardController;
    public InputContoller InputContoller;
    public Cell HoldedCell;

    public override void Init()
    {
    }

    public void SetHoldedCell(Cell cell)
    {
        if (cell == null)
        {
            _selected.SetActive(false);
            HoldedCell = null;
            return;
        }

        _selected.SetActive(true);
        HoldedCell = cell;
        var pos = GridPositionToWorldPosition(cell.CellPosition);
        pos.z = 1;
        _selected.transform.position = pos;
    }

    [Button(ButtonSizes.Gigantic)]
    public void Play()
    {
        BoardController.SpawnBoard();
        SetHoldedCell(null);
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