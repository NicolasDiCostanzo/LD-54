
using UnityEngine;
using System;

public class DoorOpen : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite openDoorSprite;

    public Action OnOpenDoor;

    void Awake()
    {
        OnOpenDoor += Win;
    }

    public void OpenDoor()
    {
        spriteRenderer.sprite = openDoorSprite;

        OnOpenDoor?.Invoke();
    }

    public void Win() {
        Debug.Log("you won !");
    }
}