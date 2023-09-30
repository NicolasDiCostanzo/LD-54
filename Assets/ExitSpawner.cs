using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSpawner : MonoBehaviour
{
    [SerializeField] GameObject exitPrefab;

    public void SpawnExitAtRandomLocation()
    {
        int numberOfPositions = transform.childCount;
        int r = Random.Range(0, numberOfPositions);
        Vector3 exitPosition = transform.GetChild(r).position;

        GameObject exit = Instantiate(exitPrefab, exitPosition, Quaternion.identity);
        exit.transform.name = "Exit";
        exit.transform.SetParent(transform.GetChild(r));
    }
}
