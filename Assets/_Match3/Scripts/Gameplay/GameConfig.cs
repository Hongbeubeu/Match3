using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Data/GameConfig")]
public class GameConfig : ScriptableObject
{
    public GroupType TileGroup;
    public Vector2Int BoardSize;
}
