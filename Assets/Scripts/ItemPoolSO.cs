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

[CreateAssetMenu(menuName = "LD-54/ItemPool")]
public class ItemPoolSO : ScriptableObject
{
    public List<ItemWithLootChance> pool;
}