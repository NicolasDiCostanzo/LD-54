using System.Collections;
using UnityEngine;

public class WeatherChart : MonoBehaviour
{
    float SUN_ANGLE = -67.5f;
    float RAIN_ANGLE = -22.5f;
    float WIND_ANGLE = 22.5f;
    float STORM_ANGLE = 67.5f;

    [SerializeField]
    AnimationCurve animationCurve;

    [SerializeField]
    float duration = 1f;

    public void SetWeather(WeatherManager.WeatherState weather)
    {
        float angle = SUN_ANGLE;
        switch (weather)
        {
            case WeatherManager.WeatherState.NoWeather: angle = SUN_ANGLE; break;
            case WeatherManager.WeatherState.Rainy: angle = RAIN_ANGLE; break;
            case WeatherManager.WeatherState.MagneticStorm: angle = STORM_ANGLE; break;
        }

        StartCoroutine(nameof(RotateAnimation), Quaternion.Euler(0, 0, angle));
    }

    IEnumerator RotateAnimation(Quaternion targetRotation)
    {
        float current = 0f;

        Quaternion startRotation = transform.rotation;

        while (current < duration)
        {
            current += Time.fixedDeltaTime;
            float t = animationCurve.Evaluate(current / duration);
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }
    }
}
