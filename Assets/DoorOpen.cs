
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite openDoorSprite;

    public void OpenDoor()
    {
        spriteRenderer.sprite = openDoorSprite;

        Win();
    }

    public void Win() {
        Debug.Log("you won !");
    }
}