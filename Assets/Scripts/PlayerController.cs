using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private ItemHolder _itemHolder;
    [SerializeField]
    private TorchManager _torchManager;
    [SerializeField]
    private float _speed;

    private Vector3 m_input;

    private bool m_wantToGrab;
    private GrabbableItem m_nearestItem;

    // Update is called once per frame
    void Update()
    {
        GatherInput();

        CheckAndGrab();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void GatherInput()
    {
        m_input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.E))
        {
            m_wantToGrab = true;
        }
    }

    private void Move()
    {
        var isoRotation = Quaternion.Euler(0, 45, 0);
        var delta = isoRotation * m_input;
        _rigidbody.MovePosition(transform.position + delta * _speed * Time.deltaTime);
    }

    private void CheckAndGrab()
    {
        if (!m_wantToGrab) return;

        if (m_nearestItem == null)
        {
            Debug.Log("No item to grab");
            m_wantToGrab = false;
            return;
        }

        _itemHolder.SetItem(m_nearestItem.itemType);
        Destroy(m_nearestItem.gameObject, 0.1f);
        m_wantToGrab = false;
        m_nearestItem = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<GrabbableItem>(out var item))
        {
            Debug.Log("Near item: " + item.itemType);
            m_nearestItem = item;
        }
        if (other.TryGetComponent<ChestLoot>(out var chest))
        {
            ItemType lootedItem = chest.OpenChest();
            if (lootedItem == ItemType.Oil)
            {
                _torchManager.RestoreFuel();
            }
            else
            {
                _itemHolder.DropItem(lootedItem);
            }

            Destroy(chest.gameObject, 0.5f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<GrabbableItem>(out var item))
        {
            Debug.Log("Exiting collider: " + item.itemType);
            m_nearestItem = null;
        }
    }
}
