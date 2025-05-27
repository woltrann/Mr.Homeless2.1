using UnityEngine;

[System.Serializable]
public class DialogueEffect
{
    public string variableName; // Matches the "VariableName" field in the JSON
    public string operation; // Matches the "Operation" field in the JSON
    public string value; // Matches the "Value" field in the JSON
    public string variableType; // Matches the "VariableType" field in the JSON
}
