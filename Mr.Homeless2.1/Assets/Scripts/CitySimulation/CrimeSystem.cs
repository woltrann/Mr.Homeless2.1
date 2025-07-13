using UnityEngine;

public class CrimeSystem : MonoBehaviour
{
    public void Simulate(CityData city)
    {
        float baseCrime = 5f;
        baseCrime += city.unemploymentRate * 0.6f;
        baseCrime += city.pollutionRate * 0.2f;
        baseCrime -= city.happinessRate * 0.3f;

        city.crimeRate = Mathf.Clamp(baseCrime, 0f, 100f);

        // 1. Suç Dalgasý
        if (city.crimeRate > 70f)
        {
            Debug.LogWarning($"{city.cityName} is experiencing a crime wave!");
            city.DecreaseHappinessRate(10f);
        }
    }
}
