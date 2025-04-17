using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string actionName;
    public int Para;   public TextMeshProUGUI paraText;
    public int Saglik; public TextMeshProUGUI saglikText; public Slider SaglikSlider; private int maxSaglik = 100;
    public int Enerji; public TextMeshProUGUI enerjiText; public Slider EnerjiSlider; private int maxEnerji = 100; 
    
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
       
    }
    void Start()
    {
        Saglik = maxSaglik;
        Enerji = maxEnerji;
        Para = 0;
        UpdateSaglikBar();
        UpdateEnerjiBar();
        paraText.text = "Para= 0$";
    }
    public void RentApartment()
    {
        Debug.Log("Daire kiralandý! Para azaldý. ve oldu");
    }
    public void BuyApartment()
    {
        Debug.Log("Daire satýn alýnd! Para azaldý.");
    }
    public void RobBank()
    {
        Debug.Log("Banka soyuldu! Para arttý.");
    }
    public void OpenShop()
    {
        Debug.Log("Maðaza açýldý.");
    }
    public void GetTaxi()
    {
        Debug.Log("Taksiye çýk.");
    }
    public void CoptenYe()
    {
        Debug.Log("Çöpten yedi");
    }
    public void BanktaUyu()
    {
        Debug.Log("Bankta Uyudu");
    }

    public void ParaArtisi(int money)
    {
        Para += money;
        paraText.text = "Para= " + Para + "$";
    }
    public void ParaDususu(int money)
    {
        Para -= money;
        paraText.text = "Para= " + Para + "$";
    }
    public void SaglikArtisi(int can)
    {
        Saglik += can;
        Saglik = Mathf.Clamp(Saglik, 0, maxSaglik);
        UpdateSaglikBar();
    }
    public void SaglikDususu(int can)
    {
        Saglik -= can;
        Saglik = Mathf.Clamp(Saglik, 0, maxSaglik);
        UpdateSaglikBar();
    }
    public void EnerjiArtisi(int enerji)
    {
        Enerji += enerji;
        Enerji = Mathf.Clamp(Enerji, 0, maxEnerji);
        UpdateEnerjiBar();
    }
    public void EnerjiDususu(int enerji)
    {
        Enerji -= enerji;
        Enerji = Mathf.Clamp(Enerji, 0, maxEnerji);
        UpdateEnerjiBar();
    }

    

    private void UpdateSaglikBar()
    {SaglikSlider.value = Saglik ;}
    private void UpdateEnerjiBar()
    {EnerjiSlider.value = Enerji ;}
}
