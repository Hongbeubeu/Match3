using Sirenix.OdinInspector;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [Button]
    public void Generate()
    {
        var pos = Vector2.zero;
        for (var i = 0; i < 10; i++)
        {
            var tile = GameManager.Instance.ObjectPooler.InstantiateRandomTile();
            tile.transform.position = pos;
            pos.x += 1;
        }
    }
}