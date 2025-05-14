using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AffinityData", menuName = "Affinity/AffinityData")]
public class AffinityData : ScriptableObject
{
    [Range(-100f, 100f)]
    public float AffinityLevel;
    public FactionData FactionData;

    public void SetAffinityLevel(float value)
    {
        AffinityLevel = Mathf.Clamp(value, -100f, 100f);
    }
}
