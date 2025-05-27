using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class DialogueLoader : MonoBehaviour
{
    private Dictionary<string, DialogueData> dialogueCache = new Dictionary<string, DialogueData>();
    public DialogueData dialogueData;

    private void Awake()
    {
        PreloadDialogues();
    }

    private void PreloadDialogues()
    {
        string[] files = Directory.GetFiles(Application.streamingAssetsPath, "*.json");

        foreach (string filePath in files)
        {
            string json = File.ReadAllText(filePath);
            DialogueData tempData = JsonUtility.FromJson<DialogueData>(json);

            // Assuming DialogueData has an `id` field of type string
            if (!string.IsNullOrEmpty(tempData.id) && !dialogueCache.ContainsKey(tempData.id))
            {
                dialogueCache[tempData.id] = tempData;
            }
        }
    }

    public void LoadDialogue(string DialogueID)
    {
        if (dialogueCache.TryGetValue(DialogueID, out DialogueData cachedData))
        {
            dialogueData = cachedData;
            Debug.Log("Dialogue loaded: " + dialogueData.nodes.Count + " nodes");
        }
        else
        {
            Debug.LogError("Dialogue with ID " + DialogueID + " not found in any JSON file.");
        }
    }
}
