
using UnityEngine;

public class ChestLoot : MonoBehaviour
{
    [SerializeField]
    private ItemPoolSO itemPool;

    public ItemType OpenChest()
    {
        bool lootOK = false;

        while (!lootOK)
        {
            var candidateLoot = itemPool.pool[Random.Range(0, itemPool.pool.Count)];
            float probability = Random.value;
            if (probability > candidateLoot.probability)
                continue;

            return candidateLoot.itemType;
        }

        return ItemType.Oil;
    }
}