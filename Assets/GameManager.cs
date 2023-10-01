using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    CompassManager compassManager;
    bool gameIsPaused = false;
    [SerializeField] GameObject pauseMenu, gameOverScreen;

    void Awake()
    {
        Time.timeScale = 1;
        compassManager = FindObjectOfType<CompassManager>();

        compassManager.Init();
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
}
