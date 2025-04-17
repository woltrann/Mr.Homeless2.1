using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingUIManager : MonoBehaviour
{
    public static BuildingUIManager Instance;

    public GameObject buildingUIPanel; // Panel objesi
    public GameObject cameraUI;
    public Canvas yourCanvas; // Canvas referans�
    public Text buildingNameText;      // Bina ismi
    public Text buildingInfoText;      // Bina a��klamas�
    public Transform buttonContainer;  // Butonlar�n yerle�ece�i alan
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

            buildingNameText.text = info.buildingName;          // Panel bilgilerini g�ncelle
            buildingInfoText.text = info.buildingDescription;

            foreach (Transform child in buttonContainer)         // Eski butonlar� temizle
            {
                Destroy(child.gameObject);
            }
            foreach (BuildingButton buttonData in info.buttons)     // Yeni butonlar� olu�tur
            {
                CreateButton(buttonData.buttonText, buttonData.action);
            }
            Camera cameraComponent = cameraUI.GetComponent<Camera>();
            cameraSize = cameraComponent.orthographicSize;

            Vector3 buildingWorldPos = building.transform.position;         // Binan�n konumunu al
            Vector3 adjustedWorldPos = new(buildingWorldPos.x + xp, buildingWorldPos.y + yp, buildingWorldPos.z + zp);        // Paneli biraz yukar�da g�stermek i�in xyz eksenlerine kayd�rma ekleyelim
            Vector3 cameraWorldPos = new(buildingWorldPos.x + xc, buildingWorldPos.y + yc, buildingWorldPos.z + zc);

            RectTransform cameraRect = cameraUI.GetComponent<RectTransform>();
            cameraRect.position = cameraWorldPos;
            Camera.main.orthographicSize = sizec;

            RectTransform panelRect = buildingUIPanel.GetComponent<RectTransform>();
            panelRect.position = adjustedWorldPos;       // Pozisyonu belirle

            buildingUIPanel.SetActive(true);
            DisableBuildingInteractions();
            CameraPan.Instance.SetMovementEnabled(false); // Kameran�n hareketini devre d��� b�rak
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
            CameraPan.Instance.SetMovementEnabled(true); // Kameran�n hareketini tekrar etkinle�tir
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

    public void DisableBuildingInteractions()       // Binaya t�kland���nda t�m "Building" tagine sahip objelerin t�klanabilirli�ini pasif yap
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        foreach (GameObject building in buildings)
        {
            Collider collider = building.GetComponent<Collider>();
            {
                collider.enabled = false; // Collider'� devre d��� b�rak
            }
        }
    }

    public void EnableBuildingInteractions()        // Panel kapand���nda t�m "Building" tagine sahip objelerin t�klanabilirli�ini aktif yap
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        foreach (GameObject building in buildings)
        {
            Collider collider = building.GetComponent<Collider>();
            {
                collider.enabled = true; // Collider'� etkinle�tir
            }
        }
    }
}
