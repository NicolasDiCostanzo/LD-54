using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    CompassManager compassManager;
    TorchManager torchManager;
    CreditsManager creditsManager;
    DoorOpen exitDoor;

    void Awake()
    {
        compassManager = FindObjectOfType<CompassManager>();
        torchManager = FindObjectOfType<TorchManager>();
        creditsManager = FindObjectOfType<CreditsManager>();
        exitDoor = FindObjectOfType<DoorOpen>();

        compassManager.Init();
        torchManager.OnGameOver += GameOver;
        exitDoor.OnOpenDoor += EndCredits;
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
    }

    public void EndCredits()
    {
        // torchManager.SwitchOffTorchEndCredits();
        creditsManager.Trigger();
    }
}
