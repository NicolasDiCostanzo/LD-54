using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemHolder : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private List<ItemBase> items;

    private ItemType? m_currentItem = null;

    public bool FacingBack = false;

    public ItemType CurrentItem => m_currentItem.HasValue ? m_currentItem.Value : ItemType.None;

    public void SetItem(ItemType newItem)
    {
        if (newItem == m_currentItem)
            return;

        if (m_currentItem.HasValue)
            DropItem(m_currentItem.Value);

        UpdateSprite();

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

    public void UpdateSprite()
    {
        spriteRenderer.sprite = GetSprite(CurrentItem);
        if (FacingBack)
            spriteRenderer.transform.localPosition = new Vector3(0.12f, 0f, -0.12f);
        else
            spriteRenderer.transform.localPosition = new Vector3(0.095f, 0f, -0.095f);
    }

    private Sprite GetSprite(ItemType itemType)
    {
        ItemBase it = items.Find((ItemBase item) => itemType == item.itemType);
        if (it != null)
        {
            return FacingBack ? it.itemSpriteBack : it.itemSprite;
        }
        Debug.LogWarning("No matching item with type:" + itemType);
        return null;
    }
}
