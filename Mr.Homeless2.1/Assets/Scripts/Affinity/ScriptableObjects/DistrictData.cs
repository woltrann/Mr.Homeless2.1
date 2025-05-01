using UnityEngine;

[CreateAssetMenu(fileName = "New Districts", menuName = "Affinity/Districts")]
public class DistrictData : ScriptableObject
{
    public string districtName;
    public Vector3Int coordinates; // Haritadaki ýzgara pozisyonu
    public Color districtColor;
    public FactionData controllingFaction;
    [Range(0f, 1f)] public float heatLevel; // 0 = sakin, 1 = tehlikeli
    [TextArea]
    public string description;
}
