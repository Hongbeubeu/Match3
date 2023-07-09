using Lean.Touch;
using UnityEngine;

public class InputContoller : MonoBehaviour
{
    [SerializeField] private GameObject _selected;
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
        var pos = Camera.main.ScreenToWorldPoint(obj.ScreenPosition);
        var gridPos = GameController.Instance.WorldPositionToGridPosition(pos);
        pos = GameController.Instance.GridPositionToWorldPosition(gridPos);
        pos.z = 1f;
        _selected.transform.position = pos;
    }
}