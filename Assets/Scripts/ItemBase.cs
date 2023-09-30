using System;
using UnityEngine;

public enum ItemType
{
    Compass,
    Umbrella
}

[Serializable]
public class ItemBase
{
    public ItemType itemType;
    public Sprite itemSprite;
    public GameObject itemPrefab;
}
