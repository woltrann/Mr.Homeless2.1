using UnityEngine;

[CreateAssetMenu(fileName = "TaskData", menuName = "Player/TaskData")]
public class TaskData : ScriptableObject
{
    public string taskName;
    public string description;
    public string[] requiredItems; // Malzeme listesi
    public string[] rewardText;      // Ödül açýklamasý
}
