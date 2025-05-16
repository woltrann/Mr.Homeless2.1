using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NPCPanel : MonoBehaviour
{
    public Transform contentParent; // ScrollView > Viewport > Content
    public GameObject npcCardPrefab;

    public List<NPCs> npcList; // Inspector’dan manuel ver veya Resources.LoadAll ile çek

    void Start()
    {
        LoadNPCs();
    }

    void LoadNPCs()
    {
        foreach (NPCs npc in npcList)
        {
            GameObject card = Instantiate(npcCardPrefab, contentParent);

            card.transform.Find("TextName (1)").GetComponent<Text>().text = npc.NPCName;
            card.transform.Find("TextJob (1)").GetComponent<Text>().text = npc.NPCJob;
            card.transform.Find("TextDescription").GetComponent<Text>().text = npc.description;
            card.transform.Find("Image").GetComponent<Image>().sprite = npc.sprite;
            card.transform.Find("TextRelation (1)").GetComponent<Text>().text = npc.affinityValue.ToString();

            // Ýstersen renk veya iliþki yorumlarý da ekleyebilirsin
        }
    }
}
