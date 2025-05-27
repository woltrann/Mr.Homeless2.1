using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CallDialogue", menuName = "Dialogue/CallDialogue")]
public class CallDialogue : ScriptableObject
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
    public TaskData relatedTask;


}
