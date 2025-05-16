using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanel : MonoBehaviour
{
    public Transform contentParent; // ScrollView > Viewport > Content
    public GameObject taskCardPrefab;

    public List<TaskData> taskList;           // Inspector’dan ya da Resources’tan doldur

    private void Start()
    {
        LoadTasks();
    }

    void LoadTasks()
    {
        foreach (TaskData task in taskList)
        {
            GameObject card = Instantiate(taskCardPrefab, contentParent);

            card.transform.Find("TextTitle").GetComponent<Text>().text = task.taskName;
            card.transform.Find("TextDescription").GetComponent<Text>().text = task.description;

            string itemList = string.Join(", "+"\n", task.requiredItems);
            card.transform.Find("TextNeeds").GetComponent<Text>().text = itemList;


            string rewardList = string.Join(", "+"\n", task.rewardText);
            card.transform.Find("TextReward").GetComponent<Text>().text = rewardList;
        }
    }
}
