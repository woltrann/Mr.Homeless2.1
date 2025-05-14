using UnityEngine;
using UnityEngine.UI;

public class BuildingPanelManager : MonoBehaviour
{
    public GameObject buildingPanel;
    public Text nameText;
    public Text descriptionText;
    public Text NPCText;
    public GameObject buildingSprite;
    public Image Sprite;
    public Button button;
    public BuildingData buildingType;

    void Awake()
    {
        Sprite = buildingSprite.GetComponent<Image>();
    }
    private void OnMouseDown()
    {
        if (buildingType.IsActive) 
        {
            button.onClick.RemoveAllListeners();
            buildingPanel.SetActive(true);
            nameText.text = buildingType.BuildingName;
            descriptionText.text = buildingType.description;
            NPCText.text = buildingType.NPCs[0].NPCName;
            Sprite.sprite = buildingType.sprite;
            button.onClick.AddListener(priceup);
        }  
    }
    public void priceup()
    {
        buildingType.price *= 2;
    }
    
}
