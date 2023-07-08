using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    private int id;

    public void SetSpriteRenderer(Sprite sprite)
    {
        _renderer.sprite = sprite;
    }

    public void Destroy()
    {
        GameManager.Instance.ObjectPooler.DestroyTile(gameObject);
    }
}