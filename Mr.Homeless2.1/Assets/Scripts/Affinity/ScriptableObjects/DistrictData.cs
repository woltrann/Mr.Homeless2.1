using UnityEngine;

[CreateAssetMenu(fileName = "New District", menuName = "Affinity/Districts")]
public class DistrictData : ScriptableObject
{
    public string districtName;
    public Transform CamCoordinates;
    public Color districtColor;
    public FactionData controllingFaction;
    public BuildingData[] buildings; // Bina verileri
    [Range(0f, 1f)] public float heatLevel; // 0 = sakin, 1 = tehlikeli
    [TextArea]
    public string description;
}
