using Ultimate.Core.Runtime.Pool;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectPooler", menuName = "Data/ObjectPooler", order = 0)]
public class ObjectPooler : ScriptableObject
{
    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private TileCollection _tileCollection;

    public Tile InstantiateRandomTile(int[] ignores)
    {
        var tile = FastPoolManager.GetPool(_tilePrefab).FastInstantiate<Tile>();
        tile.SetSpriteRenderer(_tileCollection.GetRandomTile(GameManager.Instance.GameConfig.TileGroup, ignores));
        return tile;
    }

    public void DestroyTile(GameObject gameObject)
    {
        FastPoolManager.GetPool(_tilePrefab).FastDestroy(gameObject);
    }
}