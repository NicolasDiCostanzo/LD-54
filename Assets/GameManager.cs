using UnityEngine;

public class GameManager : MonoBehaviour
{
    CompassManager compassManager;
    CreditsManager creditsManager;
    DoorOpen exitDoor;
    bool gameIsPaused = false;
    [SerializeField] GameObject pauseMenu, gameOverScreen;

    void Awake()
    {
        Time.timeScale = 1;
        compassManager = FindObjectOfType<CompassManager>();
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
        creditsManager.Trigger();
    }
}
