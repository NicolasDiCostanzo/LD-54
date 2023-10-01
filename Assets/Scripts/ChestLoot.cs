
using UnityEngine;

public class ChestLoot : MonoBehaviour
{
    [SerializeField]
    private ItemPoolSO itemPool;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite openChestSprite;

    public ItemType OpenChest()
    {
        spriteRenderer.sprite = openChestSprite;

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