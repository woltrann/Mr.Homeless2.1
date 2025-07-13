using UnityEngine;

public class EconomySystem : MonoBehaviour
{
    public void Simulate(CityData city)
    {
        float taxIncome = city.population * 0.3f;
        float unemploymentPenalty = city.unemploymentRate * 200f;

        float totalIncome = taxIncome;
        float totalExpenses = unemploymentPenalty + city.crimeRate * 100f + city.pollutionRate * 50f;

        city.budget += Mathf.RoundToInt(totalIncome - totalExpenses);

        if (city.budget < 0)
            city.happinessRate -= 5f;

        city.budget = Mathf.Clamp(city.budget, -99999999, 99999999);
        city.happinessRate = Mathf.Clamp(city.happinessRate, 0f, 100f);
    }
}
