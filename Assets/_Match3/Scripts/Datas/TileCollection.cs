using UnityEngine;

[CreateAssetMenu(fileName = "TileCollections", menuName = "Data/TileCollections", order = 1)]
public class TileCollection : ScriptableObject
{
    public TileGroup[] TileGroups;

    public (Sprite, int) GetRandomTile(GroupType groupType, int[] ignores)
    {
        return TileGroups[(int) groupType].GetRandomSprite(ignores);
    }
}