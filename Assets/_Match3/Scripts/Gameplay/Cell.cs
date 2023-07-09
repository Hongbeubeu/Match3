using UnityEngine;

public class Cell
{
    public Vector2Int CellPosition;
    public Tile Tile;

    public void SetInfo(Vector2Int pos, Tile tile)
    {
        CellPosition = pos;
        Tile = tile;
    }

    public void DestroyTile()
    {
        if (Tile != null)
            Tile.Destroy();
    }
}