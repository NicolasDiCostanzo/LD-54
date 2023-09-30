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


    void Update()
    {
        Debug.Log(m_currentItem);
    }

    public void SetItem(ItemType newItem)
    {
        if (newItem == m_currentItem)
            return;

        DropItem();

        ItemBase it = items.Find((ItemBase item) => newItem == item.itemType);
        if (it != null)
        {
            spriteRenderer.sprite = it.itemSprite;
        }


        m_currentItem = newItem;
    }

    private void DropItem()
    {
        if (!m_currentItem.HasValue) return;

        ItemBase it = items.Find((ItemBase item) => m_currentItem == item.itemType);
        if (it != null)
        {
            Instantiate(it.itemPrefab, transform.position + Vector3.right, Quaternion.identity);
        }

        m_currentItem = null;
    }
}
