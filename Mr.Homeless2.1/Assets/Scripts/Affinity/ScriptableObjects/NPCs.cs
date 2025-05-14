using UnityEngine;

[CreateAssetMenu(fileName = "NPCs", menuName = "Affinity/NPCs")]
public class NPCs : ScriptableObject
{
    public string NPCName;
    public Sprite sprite;
    public string description;
    public FactionData FactionData;
    
}
