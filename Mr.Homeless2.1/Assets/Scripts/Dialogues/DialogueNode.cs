using System.Collections.Generic;

[System.Serializable]
public class DialogueNode
{
    public string id; // Matches the "Id" field in the JSON
    public string speaker; // Matches the "Speaker" field in the JSON
    public string text; // Matches the "Text" field in the JSON
    public int posX; // Matches the "PosX" field in the JSON
    public int posY; // Matches the "PosY" field in the JSON
    public List<DialogueChoice> choices; // Matches the "Choices" array in the JSON
    public DialogueCondition conditions; // Matches the "Conditions" field in the JSON
}
