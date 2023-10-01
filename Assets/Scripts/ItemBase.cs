using System;
using UnityEngine;

public enum ItemType
{
    Compass,
    Umbrella,
    Oil,
    None
}

[Serializable]
public class ItemBase
{
    public ItemType itemType;
    public Sprite itemSprite;
    public Sprite itemSpriteBack;
    public GameObject itemPrefab;
}
