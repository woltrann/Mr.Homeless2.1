using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BuildingUI : MonoBehaviour
{  
    private float currentTime = 480f;
    public static BuildingUI Instance;
    
    public TMP_Text clockText;
    public GameObject BuildingPanel, PauseMenuPanel;
    public TMP_Text nameText, timeText;
    public Button actionButton;

    void Awake() => Instance = this;

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
        timeText.text = $"Varış süresi: {time} saniye";

        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(() =>
        {
            onClick?.Invoke();
            BuildingPanel.SetActive(false);
        });
        BuildingPanel.SetActive(true);
    }

    public void Pause() { Time.timeScale = 0f; PauseMenuPanel.SetActive(true); }
    public void Resume() { Time.timeScale = 1f; PauseMenuPanel.SetActive(false); }
}
