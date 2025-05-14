using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "buildings", menuName = "Affinity/buildings")]
public class BuildingData : ScriptableObject
{
    public string BuildingName;
    public Sprite sprite;
    public string description;
    public NPCs [] NPCs;
    public int price;
    public bool IsActive = false;
}
