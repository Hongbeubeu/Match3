using System;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct TileGroup
{
    public GroupType GroupType;
    public Sprite[] Sprites;

    public Sprite GetRandomSprite()
    {
        if (Sprites.IsNullOrEmpty())
        {
            Debug.LogError($"{GroupType} has no sprites");
            return null;
        }

        var random = Random.Range(0, Sprites.Length);
        return Sprites[random];
    }
}