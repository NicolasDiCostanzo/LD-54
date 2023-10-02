using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    CompassManager compassManager;
    CreditsManager creditsManager;
    WeatherManager weatherManager;
    DoorOpen exitDoor;
    bool gameIsPaused = false;
    [SerializeField] GameObject pauseMenu, gameOverScreen;

    [Range(1, 4)] 
    [SerializeField]
    float minCaillouxScaleRange, maxCaillouxScaleRange, minTreeScaleRange, maxTreeScaleRange;


    void Awake()
    {
        Time.timeScale = 1;
        compassManager = FindObjectOfType<CompassManager>();
        creditsManager = FindObjectOfType<CreditsManager>();
        weatherManager = FindObjectOfType<WeatherManager>();
        exitDoor = FindObjectOfType<DoorOpen>();

        compassManager.Init();
        exitDoor.OnOpenDoor += EndCredits;

        InitRocksScales();
    }

    void InitRocksScales()
    {
        string[] keywords = { "Cailloux" };

        List<GameObject> matchingGameObjects = new List<GameObject>();

        GameObject[] allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allGameObjects)
        {
            if (keywords.Any(keyword => obj.name.Contains(keyword)) && obj.name != "CaillouxInside" && obj.name != "TreeInside")
            {
                float scale = Random.Range(minCaillouxScaleRange, maxCaillouxScaleRange);
                obj.transform.localScale = new Vector3(scale, scale, scale);

                float rotation = Random.Range(0, 360);
                obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, obj.transform.eulerAngles.y, rotation);
            }
        }
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
        weatherManager.gameObject.SetActive(false);
        creditsManager.Trigger();
    }
}
