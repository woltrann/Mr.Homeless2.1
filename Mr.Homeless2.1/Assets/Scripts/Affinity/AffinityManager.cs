using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FactionAffinity
{
    public FactionData faction;
    [Range(-100, 100)]
    public int affinity; // -100 = düþman, 0 = nötr, 100 = dost
}

public class AffinityManager : MonoBehaviour
{
    public List<FactionAffinity> affinities = new List<FactionAffinity>();

    public int GetAffinity(FactionData faction)
    {
        foreach (var entry in affinities)
        {
            if (entry.faction == faction)
                return entry.affinity;
        }
        return 0; // default: nötr
    }

    public void ChangeAffinity(FactionData faction, int amount)
    {
        foreach (var entry in affinities)
        {
            if (entry.faction == faction)
            {
                entry.affinity = Mathf.Clamp(entry.affinity + amount, -100, 100);
                return;
            }
        }

        // Eðer yoksa, ekle
        affinities.Add(new FactionAffinity
        {
            faction = faction,
            affinity = Mathf.Clamp(amount, -100, 100)
        });
    }
}
