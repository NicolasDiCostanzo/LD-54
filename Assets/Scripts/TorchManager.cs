using System;
using UnityEngine;

public class TorchManager : MonoBehaviour
{
    WeatherManager weatherManager;
    ItemHolder itemHolder;
    GameManager gameManager;

    Light torch;

    public float fuel = 100;
    [SerializeField] float fuelComsuptionSpeed, fuelRestoreAmount, flickeringRange, flickeringFrq, minRange = 2.5f, startingFuel;
    float ratio, startingRange, timeTillFlickering, currentRange;

    bool isSwitchedOn = true;

    void _init()
    {
        startingFuel = fuel;
        torch = GetComponentInChildren<Light>();
        startingRange = torch.range;
        currentRange = startingRange;
        ratio = startingRange / fuel;
        timeTillFlickering = flickeringFrq;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Awake()
    {
        weatherManager = FindObjectOfType<WeatherManager>();
        itemHolder = FindObjectOfType<ItemHolder>();
    }

    void Start()
    {
        _init();
    }

    void FixedUpdate()
    {
        _consumeFuel();

        timeTillFlickering -= Time.fixedDeltaTime;
        if (timeTillFlickering <= 0)
        {
            _torchFlickering();
            timeTillFlickering = flickeringFrq;
        }
        else
        {
            setTorchRange(currentRange);
        }
    }

    public void RestoreFuel()
    {
        fuel += fuelRestoreAmount;
        fuel = Mathf.Min(fuel, 100);
    }

    void _consumeFuel()
    {
        if (fuel <= 0)
        {
            _switchOffTorch();
            return;
        }

        float consumption = fuelComsuptionSpeed / 100f;

        if (weatherManager.CurrentWeatherState == WeatherManager.WeatherState.Rainy
        && itemHolder.CurrentItem != ItemType.Umbrella)
        {
            consumption *= 2f;
        }

        fuel -= consumption;
        currentRange = fuel * ratio;
    }

    void _torchFlickering()
    {
        float r = UnityEngine.Random.Range(-flickeringRange, flickeringRange);
        setTorchRange(currentRange + r);
    }

    void setTorchRange(float newValue)
    {
        torch.range = Mathf.Lerp(minRange, startingRange, fuel / startingFuel);
    }

    void _switchOffTorch()
    {
        torch.enabled = false;
        isSwitchedOn = false;
        gameManager.GameOver();
    }

    public void SwitchOffTorchEndCredits()
    {
        torch.enabled = false;
    }
}
