using Lean.Touch;
using UnityEngine;

public class InputContoller : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void OnEnable()
    {
        LeanTouch.OnFingerDown += HandleFingerDown;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= HandleFingerDown;
    }

    private void HandleFingerDown(LeanFinger obj)
    {
        var pos = _camera.ScreenToWorldPoint(obj.ScreenPosition);
        var gridPos = GameController.Instance.WorldPositionToGridPosition(pos);
        if (!GameController.Instance.BoardController.IsInsideBoard(gridPos))
        {
            return;
        }

        if (GameController.Instance.BoardController.GetTile(gridPos) == null)
        {
            GameController.Instance.SetHoldedCell(null);
            return;
        }

        if (GameController.Instance.HoldedCell != null)
        {
            if (gridPos == GameController.Instance.HoldedCell.CellPosition)
            {
                GameController.Instance.SetHoldedCell(null);
                return;
            }

            var preHold = GameController.Instance.HoldedCell.CellPosition;
            GameController.Instance.SetHoldedCell(GameController.Instance.BoardController.GetCell(gridPos));
            GameController.Instance.BoardController.SwapTile(preHold, GameController.Instance.HoldedCell.CellPosition);
        }
        else
        {
            GameController.Instance.SetHoldedCell(GameController.Instance.BoardController.GetCell(gridPos));
        }
    }
}