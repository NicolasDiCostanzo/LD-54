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
    bool gameIsPaused = false;
    [SerializeField] GameObject pauseMenu, gameOverScreen;

    void Awake()
    {
        Time.timeScale = 1;
        compassManager = FindObjectOfType<CompassManager>();
        torchManager = FindObjectOfType<TorchManager>();
        creditsManager = FindObjectOfType<CreditsManager>();
        exitDoor = FindObjectOfType<DoorOpen>();

        compassManager.Init();
        exitDoor.OnOpenDoor += EndCredits;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        gameIsPaused = !gameIsPaused;
        Time.timeScale = gameIsPaused ? 0 : 1;
        pauseMenu.SetActive(gameIsPaused);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    public void EndCredits()
    {
        // torchManager.SwitchOffTorchEndCredits();
        creditsManager.Trigger();
    }
}
