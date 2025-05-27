using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialogueLoader loader; // Reference to the DialogueLoader
    private DialogueData currentDialogue; // Holds the current dialogue data
    private DialogueNode currentNode; // Tracks the current node in the dialogue
    private string startNodeId; // Tracks the starting node ID

    void Start()
    {
        // Initialize the starting node ID (you can set this dynamically if needed)
        startNodeId = loader.dialogueData.startNodeId;

        // Start the dialogue
        StartDialogue(startNodeId);
    }

    public void StartDialogue(string dialogueId)
    {
        // Find the starting node based on the startNodeId
        currentNode = loader.dialogueData.nodes.Find(n => n.id == startNodeId);

        // Show the starting node
        ShowNode(currentNode);
    }

    void ShowNode(DialogueNode node)
    {
        if (node == null)
        {
            Debug.LogError("Node is null. Cannot display dialogue.");
            return;
        }

        // Display the speaker and text
        Debug.Log($"{node.speaker}: {node.text}");

        // Display the choices
        for (int i = 0; i < node.choices.Count; i++)
        {
            Debug.Log($"{i + 1}. {node.choices[i].text}");
        }
    }

    public void Choose(int index)
    {
        if (currentNode == null)
        {
            Debug.LogError("Current node is null. Cannot choose a dialogue option.");
            return;
        }

        // Get the selected choice
        var choice = currentNode.choices[index];

        // Find the next node based on the choice's nextNodeId
        var nextNode = loader.dialogueData.nodes.Find(n => n.id == choice.nextNodeId);

        // Update the current node and show it
        currentNode = nextNode;
        ShowNode(currentNode);
    }
}
