using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;

public class CameraRotation : MonoBehaviour
{
    public float targetRotationX = 22f; // Hedef X rotasyonu
    public float rotationSpeed = 2f;   // D�n�� h�z� (ne kadar h�zl� de�i�ece�i)

    private float startRotationX = -9f; // Ba�lang�� X rotasyonu
    private bool isRotating = false;   // D�n���n aktif olup olmad���n� kontrol etmek i�in

    public GameObject ayarlarPanel;
    public Button devamet;
    public Button yenioyun;
    public Button ayarlar;
    public Button emek;

    public AudioSource musicSource; // M�zik �alan AudioSource
    public Slider volumeSlider;     // Ses kontrol� i�in Slider
    public AudioMixer audioMixer;  // Audio Mixer referans�
    public Slider mixervolumeSlider;   // Slider referans�
    private const string MIXER_MUSIC = "SeslerVolume";
    public AudioSource atesaudio;
    void Awake()
    {
        ayarlarPanel.SetActive(false);
    }
    void Start()
    {
        Vector3 initialRotation = transform.eulerAngles;// Kameran�n ba�lang�� X rotasyonunu ayarla
        initialRotation.x = startRotationX;
        transform.eulerAngles = initialRotation;
        
        volumeSlider.value = musicSource.volume;    // Slider'�n ba�lang�� de�erini AudioSource ile e�le�tir
        volumeSlider.onValueChanged.AddListener(SetVolume);   // Slider'�n de�er de�i�ikliklerini dinle

        mixervolumeSlider.value = 1f;    // Slider'�n ba�lang�� de�erini ayarla (0 dB olarak)
        mixervolumeSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    void Update()
    {
        if (isRotating)
        {
            Vector3 currentRotation = transform.eulerAngles;// Kameran�n mevcut rotasyonunu al
            currentRotation.x = Mathf.LerpAngle(currentRotation.x, targetRotationX, Time.deltaTime * rotationSpeed);// X eksenindeki d�n��� yava��a hedefe do�ru de�i�tir
            transform.eulerAngles = currentRotation;// Yeni rotasyonu uygula
            if (Mathf.Abs(currentRotation.x - targetRotationX) < 0.1f)// Hedefe yakla�t���nda d�n��� durdur
            {
                currentRotation.x = targetRotationX; // Tam olarak hedef de�eri ayarla
                transform.eulerAngles = currentRotation;
                isRotating = false; // D�n��� durdur
            }
        }
    }

    public void StartRotation()
    {
        isRotating = true; // D�n��� ba�lat
        devamet.gameObject.SetActive(false);
        yenioyun.gameObject.SetActive(false);
        ayarlar.gameObject.SetActive(false);
        emek.gameObject.SetActive(false);
    }
    public void StartGame()// "OyunSahnesi" yerine sahnenin ad�n� yaz
    {SceneManager.LoadScene("OyunScene");}
    public void AyarlarPaneliniAc()
    {ayarlarPanel.SetActive(true); }
    public void AyarlarPaneliniKapat()
    { ayarlarPanel.SetActive(false); }

    public void SetVolume(float volume)
    { musicSource.volume = volume; }
    public void SetMasterVolume(float volume)// Mixer'da ses seviyesini ayarla (Linear -> Logaritmik dB)
    { audioMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volume) * 20); }
    public void PlaySound()
    {
        if (atesaudio != null) // E�er bir AudioSource atanm��sa
        {
            atesaudio.Play(); // Sesi �al
        }
    }
    public void SetLanguage(int localeID)
    {
        if (localeID < 0 || localeID >= LocalizationSettings.AvailableLocales.Locales.Count)
        {
            Debug.LogError("Ge�ersiz Locale ID.");
            return;
        }

        Debug.Log($"Dil de�i�tiriliyor: {LocalizationSettings.AvailableLocales.Locales[localeID].LocaleName}");
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
    }
}
