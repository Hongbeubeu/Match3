using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct TileGroup
{
    public GroupType GroupType;
    public Sprite[] Sprites;
    
    public (Sprite, int) GetRandomSprite(int[] ignores)
    {
        if (Sprites.IsNullOrEmpty())
        {
            Debug.LogError($"{GroupType} has no sprites");
            return (null, -1);
        }

        var random = Random.Range(0, Sprites.Length);
        while (ignores.Contains(random))
        {
            random = Random.Range(0, Sprites.Length);
        }

        return (Sprites[random], random);
    }
}