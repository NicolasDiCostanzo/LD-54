using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeatherParam
{
    public WeatherManager.WeatherState state;
    public float averageDuration;
    [Range(0, 1)]
    public float probability;
}

[CreateAssetMenu(menuName = "LD-54/WeatherParametersSO")]
public class WeatherParametersSO : ScriptableObject
{
    public List<WeatherParam> parameters = new();
}