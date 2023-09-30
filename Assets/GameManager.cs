using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    ExitSpawner exitSpawner;
    CompassManager compassManager;

    void Awake()
    {
        exitSpawner = FindObjectOfType<ExitSpawner>();
        compassManager = FindObjectOfType<CompassManager>();

        exitSpawner.SpawnExitAtRandomLocation();
        compassManager.Init();
    }
}
