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

    private Animator _animation;
    public AudioSource _audioSource;

    private static readonly int IdleAnimation = Animator.StringToHash("IdleAnimation");
    private static readonly int RunAnimation = Animator.StringToHash("RunAnimation");
    private static readonly int RunAnimationBack = Animator.StringToHash("RunAnimationBack");

    private bool firstOil = true;
    private bool firstCompass = true;
    private bool firstUmbrella = true;

    public AudioManager _audioManager;

    [SerializeField]
    private ItemPopUpClose oilPopUp;
    [SerializeField]
    private ItemPopUpClose compassPopUp;
    [SerializeField]
    private ItemPopUpClose umbrellaPopUp;

    void Awake()
    {
        _animation = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

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
        if (m_input == Vector3.zero)
        {
            _audioSource.enabled = false;
            _animation.CrossFade(IdleAnimation, 0, 0);
            _itemHolder.FacingBack = false;
        }
        else if (m_input.z < 0f)
        {
            _audioSource.enabled = true;
            _animation.CrossFade(RunAnimation, 0, 0);
            _itemHolder.FacingBack = false;
        }
        else
        {
            _audioSource.enabled = true;
            _animation.CrossFade(RunAnimationBack, 0, 0);
            _itemHolder.FacingBack = true;
        }
        _itemHolder.UpdateSprite();
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

            RequireHelpPopup(lootedItem);

            if (lootedItem == ItemType.Oil)
            {
                _audioManager.PlaySound("Fire");
                _torchManager.RestoreFuel();
            }
            else
            {
                _itemHolder.DropItem(lootedItem);
            }

            Destroy(chest.gameObject, 0.5f);
        }
        if (other.TryGetComponent<DoorOpen>(out var door))
        {
            door.OpenDoor();
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

    private void RequireHelpPopup(ItemType newItem)
    {
        switch (newItem)
        {
            case ItemType.Compass:
                {
                    if (firstCompass)
                    {
                        firstCompass = false;
                        compassPopUp.gameObject.SetActive(true);
                    }
                    break;
                }
            case ItemType.Oil:
                {
                    if (firstOil)
                    {
                        firstOil = false;
                        oilPopUp.gameObject.SetActive(true);
                    }
                    break;
                }
            case ItemType.Umbrella:
                {
                    if (firstUmbrella)
                    {
                        firstUmbrella = false;
                        umbrellaPopUp.gameObject.SetActive(true);
                    }
                    break;
                }
            default: break;
        }
    }
}
