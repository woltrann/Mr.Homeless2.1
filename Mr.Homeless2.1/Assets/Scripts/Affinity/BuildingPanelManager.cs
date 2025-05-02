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
    public buildings buildingType;

    void Awake()
    {
        Sprite = buildingSprite.GetComponent<Image>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        button.onClick.RemoveAllListeners();
        buildingPanel.SetActive(true);
        nameText.text = buildingType.name;
        descriptionText.text = buildingType.description;
        NPCText.text = buildingType.NPCs[0].name;
        Sprite.sprite = buildingType.sprite;
        button.onClick.AddListener(priceup);


    }
    public void priceup()
    {
        buildingType.price *= 2;

    }
    
}
