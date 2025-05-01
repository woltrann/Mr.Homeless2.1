using UnityEngine;

[CreateAssetMenu(fileName = "New Faction", menuName = "Affinity/Faction")]
public class FactionData : ScriptableObject
{
    public string factionName;
    public Color factionColor;
    public Sprite factionIcon;
    [TextArea]
    public string description;
}
