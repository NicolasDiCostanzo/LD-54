using UnityEngine;

public class WeatherManager : MonoBehaviour
{

    public enum WeatherState
    {
        NoWeather,
        Rainy,
        MagneticStorm
    }

    [SerializeField]
    private WeatherChart _weatherChart;

    [SerializeField]
    WeatherState _currentWeatherState;

    [SerializeField]
    WeatherParametersSO weatherParams;

    private WeatherParam _currentWeatherParam;
    private float _currentTimer = 0f;

    public WeatherState CurrentWeatherState => _currentWeatherState;

    void Start()
    {
        // warning: state order should the same as in the scriptable object!
        _currentWeatherParam = weatherParams.parameters[(int)_currentWeatherState];
        _weatherChart.SetWeather(_currentWeatherState);
    }

    void FixedUpdate()
    {
        _currentTimer += Time.deltaTime;
        if (_currentTimer > _currentWeatherParam.averageDuration)
        {
            _currentTimer = 0f;
            ChangeWeather();
        }
    }

    private void ChangeWeather()
    {
        WeatherState newWeatherState = _currentWeatherState;
        int maxTries = 5;
        int tries = 0;
        while (newWeatherState == _currentWeatherState && maxTries > tries++)
        {
            var candidate = weatherParams.parameters[Random.Range(0, weatherParams.parameters.Count)];
            float probability = Random.value;
            if (probability > candidate.probability)
                continue;

            _currentWeatherParam = candidate;
            _currentWeatherState = candidate.state;
        }
        Debug.Log("Switching weather to " + _currentWeatherState);
        _weatherChart.SetWeather(_currentWeatherState);
    }
}