using UnityEngine;

[CreateAssetMenu(fileName = "Trait", menuName = "Player/TraitsData")]
public class TraitsData : ScriptableObject
{
    public string TraitName;
    [Range(0, 5)]
    public int traitLevel;
}
