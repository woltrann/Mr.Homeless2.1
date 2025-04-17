using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;

public class CameraRotation : MonoBehaviour
{
    public float targetRotationX = 22f; // Hedef X rotasyonu
    public float rotationSpeed = 2f;   // Dönüþ hýzý (ne kadar hýzlý deðiþeceði)

    private float startRotationX = -9f; // Baþlangýç X rotasyonu
    private bool isRotating = false;   // Dönüþün aktif olup olmadýðýný kontrol etmek için

    public GameObject ayarlarPanel;
    public Button devamet;
    public Button yenioyun;
    public Button ayarlar;
    public Button emek;

    public AudioSource musicSource; // Müzik çalan AudioSource
    public Slider volumeSlider;     // Ses kontrolü için Slider
    public AudioMixer audioMixer;  // Audio Mixer referansý
    public Slider mixervolumeSlider;   // Slider referansý
    private const string MIXER_MUSIC = "SeslerVolume";
    public AudioSource atesaudio;
    void Awake()
    {
        ayarlarPanel.SetActive(false);
    }
    void Start()
    {
        Vector3 initialRotation = transform.eulerAngles;// Kameranýn baþlangýç X rotasyonunu ayarla
        initialRotation.x = startRotationX;
        transform.eulerAngles = initialRotation;
        
        volumeSlider.value = musicSource.volume;    // Slider'ýn baþlangýç deðerini AudioSource ile eþleþtir
        volumeSlider.onValueChanged.AddListener(SetVolume);   // Slider'ýn deðer deðiþikliklerini dinle

        mixervolumeSlider.value = 1f;    // Slider'ýn baþlangýç deðerini ayarla (0 dB olarak)
        mixervolumeSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    void Update()
    {
        if (isRotating)
        {
            Vector3 currentRotation = transform.eulerAngles;// Kameranýn mevcut rotasyonunu al
            currentRotation.x = Mathf.LerpAngle(currentRotation.x, targetRotationX, Time.deltaTime * rotationSpeed);// X eksenindeki dönüþü yavaþça hedefe doðru deðiþtir
            transform.eulerAngles = currentRotation;// Yeni rotasyonu uygula
            if (Mathf.Abs(currentRotation.x - targetRotationX) < 0.1f)// Hedefe yaklaþtýðýnda dönüþü durdur
            {
                currentRotation.x = targetRotationX; // Tam olarak hedef deðeri ayarla
                transform.eulerAngles = currentRotation;
                isRotating = false; // Dönüþü durdur
            }
        }
    }

    public void StartRotation()
    {
        isRotating = true; // Dönüþü baþlat
        devamet.gameObject.SetActive(false);
        yenioyun.gameObject.SetActive(false);
        ayarlar.gameObject.SetActive(false);
        emek.gameObject.SetActive(false);
    }
    public void StartGame()// "OyunSahnesi" yerine sahnenin adýný yaz
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
        if (atesaudio != null) // Eðer bir AudioSource atanmýþsa
        {
            atesaudio.Play(); // Sesi çal
        }
    }
    public void SetLanguage(int localeID)
    {
        if (localeID < 0 || localeID >= LocalizationSettings.AvailableLocales.Locales.Count)
        {
            Debug.LogError("Geçersiz Locale ID.");
            return;
        }

        Debug.Log($"Dil deðiþtiriliyor: {LocalizationSettings.AvailableLocales.Locales[localeID].LocaleName}");
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
    }
}
