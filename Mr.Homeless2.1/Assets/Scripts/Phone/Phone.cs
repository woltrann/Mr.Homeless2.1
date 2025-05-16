using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using UnityEngine.EventSystems;
using System.Xml.Linq;

public class Phone : MonoBehaviour
{
    public static Phone Instance;
    private Animator phoneAnimator;
    private bool phoneBool = true;
    public Button MenuFirstbutton;

    public GameObject PauseMenuPanel, phonePanel;
    public GameObject[] phonePanels;


    void Awake() => Instance = this;
    void Start()
    {
        phoneAnimator = phonePanel.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {    
            Pause(); 
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (phoneBool)
            {
                phoneBool = false;
                phoneAnimator.SetBool("PhoneBool", true);
            }
            else
            {
                phoneBool = true;
                phoneAnimator.SetBool("PhoneBool", false);
            }

        }
    }
    public void MenuPanels(int x)
    {
        switch (x)
        {
            case 0: phonePanels[0].SetActive(true); break;
            case 1: phonePanels[1].SetActive(true); break;
            case 2: phonePanels[2].SetActive(true); break;
            default: foreach (var obj in phonePanels) obj.SetActive(false); break;

        }
    }
    public void Pause() { if(PauseMenuPanel.activeSelf)Time.timeScale = 1f;else Time.timeScale = 0f; PauseMenuPanel.SetActive(!PauseMenuPanel.activeSelf); }
}
