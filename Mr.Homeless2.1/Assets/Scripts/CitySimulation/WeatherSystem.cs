using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    public void Simulate(CityData city)
    {
        city.rainfallRate = Random.Range(0f, 200f);

        if (city.rainfallRate > 150f)
        {
            city.pollutionRate += 5f;
            city.happinessRate -= 3f;
        }

        city.pollutionRate = Mathf.Clamp(city.pollutionRate, 0f, 100f);
        city.happinessRate = Mathf.Clamp(city.happinessRate, 0f, 100f);
    }
}
