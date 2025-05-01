using System.Collections.Generic;
using UnityEngine;

public class DistrictManager : MonoBehaviour
{

    public GameObject districtViewPrefab;
    public Transform mapParent;
    
    // Haritadaki her bir bölgeyi temsil eden DistrictData nesneleri
    public List<DistrictData> allDistricts;

    public DistrictData GetDistrictByCoord(Vector3Int coord)
    {
        foreach (var d in allDistricts)
        {
            if (d.coordinates == coord)
                return d;
        }
        return null;
    }

    public void ChangeHeat(DistrictData district, float amount)
    {
        district.heatLevel = Mathf.Clamp01(district.heatLevel + amount);
        // Burada olay tetiklenebilir, UI güncellenebilir
    }

    public void ChangeControl(DistrictData district, FactionData newFaction)
    {
        district.controllingFaction = newFaction;
    }

    

    void Start()
    {
        foreach (var district in allDistricts)
        {
            var view = Instantiate(districtViewPrefab, mapParent);
            view.GetComponent<DistrictView>().Init(district);
        }
    }

}
