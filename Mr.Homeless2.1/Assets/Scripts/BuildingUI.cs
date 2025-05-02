using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;


public class BuildingUI : MonoBehaviour
{  
    private float currentTime = 480f;
    public static BuildingUI Instance;
    private Animator phoneAnimator;
    
    public TMP_Text clockText;
    public GameObject BuildingPanel, BuildingPanel2, PauseMenuPanel, phonePanel;
    public TMP_Text nameText, timeText;
    public Button actionButton;
    private bool phoneBool = true;

    void Awake() => Instance = this;
    void Start()
    {
        //phonePanel.SetActive(false);
        phoneAnimator = phonePanel.GetComponent<Animator>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) Pause();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (phoneBool)
            {
                phoneBool= false;               
                phoneAnimator.SetBool("PhoneBool", true);
            }
            else
            {
                phoneBool = true;
                phoneAnimator.SetBool("PhoneBool", false);

            }

        }
    }
    public void Clock(float time)   //Binalar arası seyahatta geçen süreyi gösterir
    {
        currentTime += time;
        int totalSeconds = Mathf.FloorToInt(currentTime);
        int hours = (totalSeconds / 60) % 24;
        int minutes = totalSeconds % 60;
        clockText.text = $"{hours:D2}:{minutes:D2}";
    }

    public void Show(string name, float time, Action onClick)   //Binaya tıkladığında bina adını ve bilgilerini gösteren paneli açar
    {
        nameText.text = name;
        timeText.text = $"Varış süresi: {time} dakika";

        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(() =>
        {
            onClick?.Invoke();
            BuildingPanel.SetActive(false);
        });
        BuildingPanel.SetActive(true);
    }
    public void BuildingPanel2Show() { BuildingPanel2.SetActive(true); }
    public void BuildingPanel2Hide() { BuildingPanel2.SetActive(false); }
    public void Pause() { Time.timeScale = 0f; PauseMenuPanel.SetActive(true); }
    public void Resume() { Time.timeScale = 1f; PauseMenuPanel.SetActive(false); }


}
