using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ExitSpawner exitSpawner;
    CompassManager compassManager;
    TorchManager torchManager;

    void Awake()
    {
        exitSpawner = FindObjectOfType<ExitSpawner>();
        compassManager = FindObjectOfType<CompassManager>();
        torchManager = FindObjectOfType<TorchManager>();

        exitSpawner.SpawnExitAtRandomLocation();
        compassManager.Init();
        torchManager.OnGameOver += GameOver;
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
    }
}
