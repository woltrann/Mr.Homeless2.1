using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public string PlayerName;
    public int PlayerMaxHealth;
    public int PlayerHealth;

    public int Money;
    public TraitsData[] traits;
    public AffinityData[] affinities;

}
