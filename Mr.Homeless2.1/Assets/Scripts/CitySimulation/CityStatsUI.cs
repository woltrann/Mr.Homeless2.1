using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CityStatsUI : MonoBehaviour
{
    public CityData cityData;
    public TextMeshProUGUI statsText;

    void Update()
    {
        statsText.text = $"{cityData.cityName}\n" +
                         $"Population: {cityData.population}\n" +
                         $"Budget: ${cityData.budget}\n" +
                         $"Happiness: {cityData.happinessRate:F1}%\n" +
                         $"Crime Rate: {cityData.crimeRate:F1}%\n" +
                         $"Pollution: {cityData.pollutionRate:F1}%\n" +
                         $"Corruption: {cityData.corruptionRate:F1}%\n" +
                         $"Unemployment: {cityData.unemploymentRate:F1}%\n" +
                         $"Rainfall: {cityData.rainfallRate:F1}mm";
    }

}
