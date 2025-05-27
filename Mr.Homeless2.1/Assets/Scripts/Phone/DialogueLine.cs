using UnityEngine;

[System.Serializable] // Böylece Unity Inspector'da görünür
public class DialogueLine
{
    public string speakerName;
    public string text;
    public AudioClip voiceClip;
    public float autoAdvanceDelay = 0f;
}
