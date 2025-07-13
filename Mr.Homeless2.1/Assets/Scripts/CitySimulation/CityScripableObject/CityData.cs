using UnityEngine;

[CreateAssetMenu(fileName = "CityData", menuName = "City/City Data")]
public class CityData : ScriptableObject
{
    [Header("Basic Info")]
    public string cityName = "Sample City";
    public int population = 500000;
    public int budget = 50000000;

    [Header("Core Rates")]
    [Range(0, 100)] public float happinessRate = 70f;  // Master parametre
    [Range(0, 100)] public float crimeRate = 25f;
    [Range(0, 100)] public float pollutionRate = 25f;
    [Range(0, 100)] public float corruptionRate = 10f; 

    [Header("City Factors")]
    [Range(0, 100)] public float unemploymentRate = 8f;

    [Header("Weather")]
    public float rainfallRate;  // Basit hava olaylarý tetiklemek için tek parametre

    [Header("Temporary Flags (for event chaining)")]
    public bool policeMoraleActive = false;
    public bool protestRecentlySuppressed = false;

    public void DecreaseHappinessRate(float value)
    {
        happinessRate -= value;
    }
}
