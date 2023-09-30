using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemHolder : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private List<ItemBase> items;

    private ItemType? m_currentItem = null;

    public void SetItem(ItemType newItem)
    {
        if (newItem == m_currentItem)
            return;

        if (m_currentItem.HasValue)
            DropItem(m_currentItem.Value);

        ItemBase it = items.Find((ItemBase item) => newItem == item.itemType);
        if (it != null)
        {
            spriteRenderer.sprite = it.itemSprite;
        }


        m_currentItem = newItem;
    }

    public void DropItem(ItemType itemType)
    {
        ItemBase it = items.Find((ItemBase item) => itemType == item.itemType);
        if (it != null)
        {
            Instantiate(it.itemPrefab, transform.position + Vector3.right, Quaternion.identity);
        }
    }
}
