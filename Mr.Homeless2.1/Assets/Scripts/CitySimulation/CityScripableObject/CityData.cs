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
        happinessRate = Mathf.Clamp(happinessRate, 0f, 100f);
    }

    public void IncreaseHappinessRate(float value)
    {
        happinessRate += value;
        happinessRate = Mathf.Clamp(happinessRate, 0f, 100f);
    }
    public void DecreaseCrimeRate(float value)
    {
        crimeRate -= value;
        crimeRate = Mathf.Clamp(crimeRate, 0f, 100f);
    }
    public void IncreaseCrimeRate(float value)
    {
        crimeRate += value;
        crimeRate = Mathf.Clamp(crimeRate, 0f, 100f);
    }
    public void DecreasePollutionRate(float value)
    {
        pollutionRate -= value;
        pollutionRate = Mathf.Clamp(pollutionRate, 0f, 100f);
    }
    public void IncreasePollutionRate(float value)
    {
        pollutionRate += value;
        pollutionRate = Mathf.Clamp(pollutionRate, 0f, 100f);
    }
    public void DecreaseCorruptionRate(float value)
    {
        corruptionRate -= value;
        corruptionRate = Mathf.Clamp(corruptionRate, 0f, 100f);
    }
    public void IncreaseCorruptionRate(float value)
    {
        corruptionRate += value;
        corruptionRate = Mathf.Clamp(corruptionRate, 0f, 100f);
    }
    public void DecreaseUnemploymentRate(float value)
    {
        unemploymentRate -= value;
        unemploymentRate = Mathf.Clamp(unemploymentRate, 0f, 100f);
    }
    public void IncreaseUnemploymentRate(float value)
    {
        unemploymentRate += value;
        unemploymentRate = Mathf.Clamp(unemploymentRate, 0f, 100f);
    }
    public void DecreasePopulation(int value)
    {
        population -= value;
        population = Mathf.Max(0, population);
    }
    public void IncreasePopulation(int value)
    {
        population += value;
        population = Mathf.Max(0, population);
    }
    public void DecreaseBudget(int value)
    {
        budget -= value;
        budget = Mathf.Clamp(budget, -999999999, 999999999);
    }
    public void IncreaseBudget(int value)
    {
        budget += value;
        budget = Mathf.Clamp(budget, -999999999, 999999999);
    }
    public void ResetCityData()
    {
        cityName = "Sample City";
        population = 500000;
        budget = 50000000;

        happinessRate = 70f;
        crimeRate = 25f;
        pollutionRate = 25f;
        corruptionRate = 10f;

        unemploymentRate = 8f;

        rainfallRate = 0f;

        policeMoraleActive = false;
        protestRecentlySuppressed = false;
    }
}
