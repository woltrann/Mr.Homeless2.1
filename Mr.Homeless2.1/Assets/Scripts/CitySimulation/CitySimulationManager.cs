using UnityEngine;

public class CitySimulationManager : MonoBehaviour
{
    public CityData cityData;


    public EconomySystem economySystem;
    public CrimeSystem crimeSystem;
    //public HealthSystem healthSystem;
    public WeatherSystem weatherSystem;
    public EventSystem eventSystem;

    public float simulationTickInterval = 2f;
    private float timer = 0f;


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= simulationTickInterval)
        {
            timer = 0f;
            SimulateTick();
        }
    }

    void SimulateTick()
    {
        economySystem?.Simulate(cityData);
        crimeSystem?.Simulate(cityData);
        weatherSystem?.Simulate(cityData);
        eventSystem?.CheckEvents(cityData);
    }

}
