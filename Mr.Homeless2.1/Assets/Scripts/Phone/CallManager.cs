using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CallManager : MonoBehaviour
{
    public Text speakerNameText;     // Ýsim etiketi
    public Text dialogueText;        // Konuþma metni
    public Button nextButton;        // Ýleri butonu
    public TaskData taskToAddAfterCall; // Inspector'dan atanacak görev
    public TaskPanel taskPanel;
    public GameObject taskAddPanel;

    //public AudioSource audioSource;  // Ses oynatýcý

    private CallDialogue currentDialogue;
    private int currentLineIndex = 0;
    private Coroutine typingCoroutine;

    private bool isWaitingForInput = false;
    public float typingSpeed = 0.05f; // Harf harf yazma hýzý

    private void Start()
    {
        nextButton.onClick.AddListener(OnNextButtonPressed);
        HideDialogueUI();
    }

    public void StartCall(CallDialogue dialogue)
    {
        currentDialogue = dialogue;
        currentLineIndex = 0;
        ShowDialogueUI();
        ShowLine(currentLineIndex);
        taskToAddAfterCall = dialogue.relatedTask; // otomatik olarak
    }

    private void ShowLine(int index)
    {
        if (index >= currentDialogue.dialogueLines.Count)
        {
            EndCall();
            return;
        }

        DialogueLine line = currentDialogue.dialogueLines[index];
        speakerNameText.text = line.speakerName;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(line.text));

        //if (line.voiceClip != null)
        //{
        //    audioSource.clip = line.voiceClip;
        //    audioSource.Play();
        //}

        if (line.autoAdvanceDelay > 0f)
        {
            isWaitingForInput = false;
            StartCoroutine(AutoAdvance(line.autoAdvanceDelay));
        }
        else
        {
            isWaitingForInput = true;
            nextButton.gameObject.SetActive(true);
        }
    }

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach (char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    private IEnumerator AutoAdvance(float delay)
    {
        nextButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(delay);
        currentLineIndex++;
        ShowLine(currentLineIndex);
    }
    private void OnNextButtonPressed()
    {
        if (!isWaitingForInput)
            return;

        currentLineIndex++;
        ShowLine(currentLineIndex);
    }


    private void EndCall()
    {
        HideDialogueUI();

        if (taskToAddAfterCall != null && taskPanel != null)
        {
            if (!taskPanel.taskList.Contains(taskToAddAfterCall))
            {
                taskPanel.taskList.Add(taskToAddAfterCall);

                taskPanel.Instance.LoadTasks();
                StartCoroutine(HideTaskAddedMessageAfterDelay(1f));

                Debug.Log("Görev eklendi: " + taskToAddAfterCall.taskName);
            }
        }

        Debug.Log("Konuþma bitti. Görev baþlatýlabilir.");
    }

    private void ShowDialogueUI()
    {
        speakerNameText.transform.parent.gameObject.SetActive(true);
    }
    private void HideDialogueUI()
    {
        speakerNameText.transform.parent.gameObject.SetActive(false);
    }
    private IEnumerator HideTaskAddedMessageAfterDelay(float delay)
    {
        taskAddPanel.SetActive(true);
        yield return new WaitForSeconds(delay);
        taskAddPanel.SetActive(false);

    }
}
