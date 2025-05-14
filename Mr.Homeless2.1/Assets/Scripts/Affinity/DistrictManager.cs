using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistrictManager : MonoBehaviour
{

    // Haritadaki her bir bölgeyi temsil eden DistrictData nesneleri
    public DistrictData district;
    public float HeightOffset = -10f; // Yükseklik ofseti
    public GameObject ExitButton;

    [SerializeField] private PlayerController playerController;

    private Button btn;
    private GameObject Player;
    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        district.CamCoordinates = GameObject.Find("CamPoint").transform;
        Player = GameObject.FindGameObjectWithTag("Player");
        btn = ExitButton.GetComponent<Button>();
        print(district.CamCoordinates);
    }
    public void ChangeHeat(DistrictData district, float amount)
    {
        district.heatLevel = Mathf.Clamp01(district.heatLevel + amount);
        // Burada olay tetiklenebilir, UI güncellenebilir
    }

    public void ChangeControl(DistrictData district, FactionData newFaction)
    {
        district.controllingFaction = newFaction;
    }

    private void OnMouseDown()
    {
        if(playerController != null)
        {
            playerController.SetLastCamTransform();
            playerController.SetMoveAbleSituation();

            ExitButton.SetActive(true); // Çýkýþ butonunu aktif et
            btn.onClick.AddListener(ExitFromDistrict);
            foreach (var building in district.buildings)
            {
                building.IsActive = true;
            }

            print("District clicked: " + district.districtName);

            Player.transform.position = district.CamCoordinates.position;
            Player.transform.position += new Vector3(0, HeightOffset, 0); // Yüksekliði ayarlamak için
            Player.transform.rotation = district.CamCoordinates.rotation;

            print("Camera moved to: " + district.CamCoordinates);
        }
        
    }

    public void ExitFromDistrict()
    {
        if (playerController != null)
        {
            playerController.GetLastCamTransform();
            playerController.SetMoveAbleSituation();

            foreach (var building in district.buildings)
            {
                building.IsActive = false;
            }

            print("District exited: " + district.districtName);
            btn.onClick.RemoveAllListeners();
            ExitButton.SetActive(false); // Çýkýþ butonunu pasif et
        }
            
    }

}
