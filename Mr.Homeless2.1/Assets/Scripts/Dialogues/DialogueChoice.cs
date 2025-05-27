using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueChoice
{
    public string text; // Matches the "Text" field in the JSON
    public string nextNodeId; // Matches the "NextNodeId" field in the JSON
    public List<DialogueCondition> conditions; // Matches the "Conditions" array in the JSON
    public List<DialogueEffect> effects; // Matches the "Effects" array in the JSON
}
