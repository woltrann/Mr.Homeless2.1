using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //public CallDialogue[] dialogues;
    public CallDialogue dialogues;
    public DialogueData dialogueData;

    public TextMeshProUGUI Speaker;
    public TextMeshProUGUI Text;

    public Button choice_1;
    public Button choice_2;
    public Button choice_3;

    private DialogueNode currentNode;
    private void Start()
    {
        dialogueData = dialogues.data;
        currentNode = dialogueData.nodes[0];
    }
    //public void CurrentDialogue(string dialogueID)
    //{
    //    foreach (CallDialogue dialogue in dialogues)
    //    {
    //        if (dialogue.data.id == dialogueID)
    //        {
    //            dialogueData = dialogue.data;
    //            currentNode = dialogueData.nodes[0];
    //        }
    //    }
    //}
    private void Update()
    {
        Speaker.text = currentNode.speaker;
        Text.text = currentNode.text;

        choice_1.GetComponentInChildren<TextMeshProUGUI>().text = currentNode.choices[0].text;
        choice_2.GetComponentInChildren<TextMeshProUGUI>().text = currentNode.choices[1].text;
        if (currentNode.choices.Count > 2)
        choice_3.GetComponentInChildren<TextMeshProUGUI>().text = currentNode.choices[2].text;

    }

    public void GetChoice(int index)
    {
        currentNode = FindNextChoice(index);
    }

    private DialogueNode FindNextChoice(int index)
    {
        for(int i = 0; i < currentNode.choices.Count; i++)
        {
            if (i == index)
            {
                string nextNode = currentNode.choices[i].nextNodeId;
                return dialogueData.nodes.Find(node => node.id == nextNode);
            }
        }
        return null;
    }
}
