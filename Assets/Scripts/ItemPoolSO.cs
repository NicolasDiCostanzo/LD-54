using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct ItemWithLootChance
{
    public ItemType itemType;
    [Range(0, 1)]
    public float probability;
}

[CreateAssetMenu(fileName = "ItemPool", menuName = "ScriptableObjects/ItemPool", order = 1)]
public class ItemPoolSO : ScriptableObject
{
    public List<ItemWithLootChance> pool;
}