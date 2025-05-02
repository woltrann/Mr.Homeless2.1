using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "buildings", menuName = "Affinity/buildings")]
public class buildings : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public string description;
    public NPCs [] NPCs;
    public int price;
}
