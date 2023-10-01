using UnityEngine;
using UnityEngine.UI;

public class ItemPopUpClose : MonoBehaviour
{
    [SerializeField]
    private Button closeButton;

    // Start is called before the first frame update
    void Start()
    {
        closeButton.onClick.AddListener(() => gameObject.SetActive(false));
    }
}
