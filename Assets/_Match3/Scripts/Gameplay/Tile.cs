using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    private int _id;

    public int ID
    {
        get => _id;
        set => _id = value;
    }

    public void SetSpriteRenderer((Sprite, int) spriteTuple)
    {
        _renderer.sprite = spriteTuple.Item1;
        _id = spriteTuple.Item2;
    }

    public void Destroy()
    {
        GameManager.Instance.ObjectPooler.DestroyTile(gameObject);
    }
}