using System.Collections.Generic;

[System.Serializable]
public class DialogueData
{
    public string id; // Matches the "Id" field in the JSON
    public string title; // Matches the "Title" field in the JSON
    public string description; // Matches the "Description" field in the JSON
    public string startNodeId; // Matches the "StartNodeId" field in the JSON
    public List<DialogueNode> nodes; // Matches the "Nodes" array in the JSON
}
