using UnityEngine;

[CreateAssetMenu(fileName = "RewardData", menuName = "Scriptable Objects/RewardData")]
public class RewardData : ScriptableObject
{
    public string rewardId;
    public string rewardName;
    public string description;
    public int amount;
    public RewardType rewardType;

    public enum RewardType
    {
        Item,
        Currency,
        Experience,
        Affinity
    }
}
