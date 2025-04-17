using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingUIManager : MonoBehaviour
{
    public static BuildingUIManager Instance;

    public GameObject buildingUIPanel; // Panel objesi
    public GameObject cameraUI;
    public Canvas yourCanvas; // Canvas referansý
    public Text buildingNameText;      // Bina ismi
    public Text buildingInfoText;      // Bina açýklamasý
    public Transform buttonContainer;  // Butonlarýn yerleþeceði alan
    public GameObject buttonPrefab;    // Buton Prefab'i
    public int xp = 0;
    public int yp = 0;
    public int zp = 0;
    public int xc = 0;
    public int yc = 0;
    public int zc = 0;
    public int sizec = 0;
    private float cameraSize = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        buildingUIPanel.SetActive(false);
    }

    public void OpenBuildingUI(GameObject building)
    {
        if (buildingUIPanel != null)
        {
            BuildingInfo info = building.GetComponent<BuildingInfo>();

            buildingNameText.text = info.buildingName;          // Panel bilgilerini güncelle
            buildingInfoText.text = info.buildingDescription;

            foreach (Transform child in buttonContainer)         // Eski butonlarý temizle
            {
                Destroy(child.gameObject);
            }
            foreach (BuildingButton buttonData in info.buttons)     // Yeni butonlarý oluþtur
            {
                CreateButton(buttonData.buttonText, buttonData.action);
            }
            Camera cameraComponent = cameraUI.GetComponent<Camera>();
            cameraSize = cameraComponent.orthographicSize;

            Vector3 buildingWorldPos = building.transform.position;         // Binanýn konumunu al
            Vector3 adjustedWorldPos = new(buildingWorldPos.x + xp, buildingWorldPos.y + yp, buildingWorldPos.z + zp);        // Paneli biraz yukarýda göstermek için xyz eksenlerine kaydýrma ekleyelim
            Vector3 cameraWorldPos = new(buildingWorldPos.x + xc, buildingWorldPos.y + yc, buildingWorldPos.z + zc);

            RectTransform cameraRect = cameraUI.GetComponent<RectTransform>();
            cameraRect.position = cameraWorldPos;
            Camera.main.orthographicSize = sizec;

            RectTransform panelRect = buildingUIPanel.GetComponent<RectTransform>();
            panelRect.position = adjustedWorldPos;       // Pozisyonu belirle

            buildingUIPanel.SetActive(true);
            DisableBuildingInteractions();
            CameraPan.Instance.SetMovementEnabled(false); // Kameranýn hareketini devre dýþý býrak
        }
    }

    public void CloseBuildingUI()
    {
        buildingUIPanel.SetActive(false);
        foreach (Transform child in buttonContainer)
        {
            Destroy(child.gameObject);
            EnableBuildingInteractions();
            Camera.main.orthographicSize = cameraSize;
            CameraPan.Instance.SetMovementEnabled(true); // Kameranýn hareketini tekrar etkinleþtir
        }
    }

    void CreateButton(string buttonText, UnityEngine.Events.UnityEvent action)
    {
        GameObject buttonObj = Instantiate(buttonPrefab, buttonContainer);
        buttonObj.GetComponentInChildren<Text>().text = buttonText;

        Button button = buttonObj.GetComponent<Button>();
        if (action != null)
        {
            button.onClick.AddListener(() => action.Invoke());
        }
    }

    public void DisableBuildingInteractions()       // Binaya týklandýðýnda tüm "Building" tagine sahip objelerin týklanabilirliðini pasif yap
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        foreach (GameObject building in buildings)
        {
            Collider collider = building.GetComponent<Collider>();
            {
                collider.enabled = false; // Collider'ý devre dýþý býrak
            }
        }
    }

    public void EnableBuildingInteractions()        // Panel kapandýðýnda tüm "Building" tagine sahip objelerin týklanabilirliðini aktif yap
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        foreach (GameObject building in buildings)
        {
            Collider collider = building.GetComponent<Collider>();
            {
                collider.enabled = true; // Collider'ý etkinleþtir
            }
        }
    }
}
