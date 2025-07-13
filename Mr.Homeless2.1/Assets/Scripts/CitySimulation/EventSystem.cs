using UnityEngine;

public class EventSystem : MonoBehaviour
{
    public void CheckEvents(CityData city)
    {
        // === NEGATİF OLAYLAR ===

        // 2. Kirlilik Boğulması
        if (city.pollutionRate > 60f)
        {
            Debug.LogWarning($"{city.cityName} is choking in pollution.");
            city.happinessRate -= 5f;
        }

        // 3. Göç Dalgası
        if (city.happinessRate < 30f && city.unemploymentRate > 20f)
        {
            Debug.LogWarning($"Migration wave from {city.cityName} due to bad living conditions.");
            city.population = Mathf.Max(0, city.population - 20000);
            city.budget -= 10000;
        }

        // 4. Garip Koku Olayı
        if (city.pollutionRate > 40f && city.rainfallRate > 100f)
        {
            Debug.LogWarning($"A strange smell spreads through {city.cityName} after heavy rain.");
            city.happinessRate -= 3f;
        }

        // 5. Gençlik Hareketi
        if (city.unemploymentRate > 30f)
        {
            Debug.LogWarning($"Youth unrest rises in {city.cityName} due to high unemployment.");
            city.happinessRate += 2f;
            city.crimeRate += 5f;
            if(Random.value < 0.2f) // %20 olasılıkla
            {
                Debug.LogWarning($"Protests erupt in {city.cityName} due to youth unrest.");
                city.happinessRate -= 10f;
                city.crimeRate += 10f;
            }
            else
            {
                Debug.Log($"{city.cityName} sees peaceful protests by the youth.");
            }
        }

        // 6. Gıda Zehirlenmesi Krizi
        if (city.pollutionRate > 70f && city.rainfallRate > 130f)
        {
            Debug.LogWarning($"Food poisoning crisis in {city.cityName}!");
            city.population = Mathf.Max(0, city.population - 2000);
            city.happinessRate -= 7f;
        }

        // 7. Toplu İşçi Grevi
        if (city.unemploymentRate > 25f && city.happinessRate < 40f)
        {
            Debug.LogWarning($"Mass worker strike hits {city.cityName}!");
            city.budget -= 25000;
            city.happinessRate -= 5f;
        }

        // 8. Sahte Yardım Kuruluşu
        if (city.corruptionRate > 40f && city.happinessRate > 60f)
        {
            Debug.LogWarning($"Fraud detected in a charity organization in {city.cityName}.");
            city.budget -= 100000;
            city.corruptionRate = Mathf.Min(100f, city.corruptionRate + 10f);
        }

        // === POZİTİF OLAYLAR ===

        // 9. Sessiz Gece
        if (city.crimeRate < 20f && city.rainfallRate < 50f)
        {
            Debug.Log($"{city.cityName} enjoys a peaceful night.");
            city.happinessRate += 5f;
            city.crimeRate = Mathf.Max(0, city.crimeRate - 2f);
        }

        // 10. Gizli Hayırsever
        if (city.happinessRate < 50f && city.budget < 0)
        {
            Debug.Log($"{city.cityName} received a donation from a mysterious benefactor.");
            city.budget += 15000;
        }

        // 11. Yatırımcılar Zirvesi
        if (city.crimeRate < 25f && city.happinessRate > 75f && city.budget > 500000)
        {
            Debug.Log($"{city.cityName} hosts an international investment summit.");
            city.budget += 100000;
            city.unemploymentRate = Mathf.Max(0, city.unemploymentRate - 3f);
        }

        // 12. Dış Göçmen Dalgası
        if (city.happinessRate > 80f && city.budget > 1000000)
        {
            Debug.Log($"{city.cityName} attracts foreign immigrants.");
            city.population += 30000;
            city.unemploymentRate += 5f;
        }

        // 13. Mahalle Dayanışması
        if (city.crimeRate < 30f && city.pollutionRate < 30f && city.rainfallRate > 70f)
        {
            Debug.Log($"{city.cityName} experiences a strong wave of local solidarity.");
            city.happinessRate += 10f;
        }

        // === DENETLEYİCİ VE ZİNCİR OLAYLAR ===

        // 14. Toplum Temizliği Günü
        if (city.pollutionRate > 40f && city.happinessRate > 50f)
        {
            Debug.Log($"{city.cityName} citizens organized a public cleanup day.");
            city.pollutionRate -= 10f;
            city.happinessRate += 5f;
        }

        // 15. Mahalle Bekçileri
        if (city.crimeRate > 30f && city.unemploymentRate < 20f)
        {
            Debug.Log($"{city.cityName} volunteers formed neighborhood patrols.");
            city.crimeRate -= 7f;
            city.happinessRate += 2f;
        }

        // 16. Polis Moral Kampı → Başarı Zinciri
        if (city.crimeRate > 50f && Random.value < 0.1f)
        {
            Debug.Log("Police morale training initiated.");
            city.policeMoraleActive = true;
        }

        if (city.policeMoraleActive)
        {
            Debug.Log("Police morale success: crime rates drop.");
            city.crimeRate -= 5f;
            city.happinessRate += 3f;
            city.policeMoraleActive = false;
        }

        // 17. Çevre Hareketi
        if (city.pollutionRate > 50f && city.happinessRate > 60f)
        {
            Debug.Log("Environmental activism reduces pollution in the city.");
            city.pollutionRate -= 8f;
            city.unemploymentRate = Mathf.Max(0, city.unemploymentRate - 2f);
        }

        // 18. Protesto Bastırıldı
        if (city.happinessRate < 50f && city.crimeRate < 30f && Random.value < 0.1f)
        {
            Debug.Log("Protests have been peacefully resolved.");
            city.happinessRate += 7f;
            city.unemploymentRate = Mathf.Max(0, city.unemploymentRate - 1f);
            city.protestRecentlySuppressed = true;
        }

        // === RASTGELE DESTEK OLAYLARI ===

        // 19. Rastgele Temizlik
        if (Random.value < 0.15f)
        {
            Debug.Log("City conducted a surprise cleanup operation.");
            city.pollutionRate -= 10f;
            city.budget -= 5000;
        }

        // 20. Yurt Dışı Fon
        if (Random.value < 0.05f && city.budget < 100000)
        {
            Debug.Log("Foreign aid fund granted to support the city's budget.");
            city.budget += 75000;
            city.unemploymentRate = Mathf.Max(0, city.unemploymentRate - 1f);
        }

        // 21. Festival Etkinliği
        if (city.happinessRate > 70f && Random.value < 0.2f)
        {
            Debug.Log("A major cultural festival boosts public morale.");
            city.happinessRate += 10f;
            city.budget -= 10000;
        }

        // === Clamp işlemleri ===
        city.happinessRate = Mathf.Clamp(city.happinessRate, 0f, 100f);
        city.pollutionRate = Mathf.Clamp(city.pollutionRate, 0f, 100f);
        city.crimeRate = Mathf.Clamp(city.crimeRate, 0f, 100f);
        city.unemploymentRate = Mathf.Clamp(city.unemploymentRate, 0f, 100f);
        city.corruptionRate = Mathf.Clamp(city.corruptionRate, 0f, 100f);
        city.population = Mathf.Max(0, city.population);
        city.budget = Mathf.Clamp(city.budget, -999999999, 999999999);
    }
}
