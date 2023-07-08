using UnityEngine;

[CreateAssetMenu(fileName = "TileCollections", menuName = "Data/TileCollections", order = 1)]
public class TileCollection : ScriptableObject
{
    public TileGroup[] TileGroups;

    public Sprite GetRandomTile(GroupType groupIndex)
    {
        return TileGroups[(int) groupIndex].GetRandomSprite();
    }
}