using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public float zoomSpeed = 0.01f;            // Yakýnlaþtýrma hýzý
    public float minZoom = 5f;                // Minimum yakýnlaþtýrma seviyesi
    public float maxZoom = 20f;               // Maksimum uzaklaþtýrma seviyesi
    public float panSpeed = 0.05f;            // Kameranýn hareket hýzý
    public Vector2 panLimitMin;               // Kameranýn gidebileceði minimum sýnýr
    public Vector2 panLimitMax;               // Kameranýn gidebileceði maksimum sýnýr

    private Vector3 lastPosition;
    private bool isDragging = false;          // Kaydýrma yapýlýp yapýlmadýðýný kontrol etmek için
    private bool isMovementEnabled = true;    // Kameranýn hareket edip etmeyeceðini kontrol eden deðiþken

    public static CameraPan Instance { get; private set; } // Singleton Instance

    void Start()
    {
        if (Instance == null)
        {
            Instance = this; // Singleton atamasý yapýlýyor
        }
        else
        {
            Debug.LogError("Birden fazla CameraPan Instance'i var!");
            Destroy(this);
        }
    }

    void Update()
    {
        if (!isMovementEnabled) return; // Eðer kamera hareketi devre dýþýysa hiçbir þey yapma

        if (Input.touchCount == 1) // Tek dokunma: Kamera hareketi veya bina týklamasý
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isDragging = false; // Kaydýrma baþlangýcýnda sýfýrla

                lastPosition = Camera.main.ScreenToWorldPoint(touch.position);      // Kamera hareketi için pozisyon kaydý
                lastPosition.y = 0;         // Sadece x ve z ekseninde hareket edelim
            }
            else if (touch.phase == TouchPhase.Moved) // Kamera hareketi
            {
                isDragging = true; // Hareket ediliyorsa kaydýrma yapýldýðýný iþaretle

                Vector3 deltaPosition = new Vector3(touch.deltaPosition.x, 0, touch.deltaPosition.y) * panSpeed;
                Vector3 newPosition = transform.position - deltaPosition; // Eksi koyduk çünkü hareket tersine çalýþýyor
                newPosition.x = Mathf.Clamp(newPosition.x, panLimitMin.x, panLimitMax.x);
                newPosition.z = Mathf.Clamp(newPosition.z, panLimitMin.y, panLimitMax.y);
                transform.position = newPosition;
            }
        }
        if (Input.touchCount == 2)  // Eðer 2 dokunma varsa 
        {
            isDragging = true;
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            float prevTouchDeltaMag = (touch0.position - touch0.deltaPosition - (touch1.position - touch1.deltaPosition)).magnitude;    // Önceki ve þimdiki pozisyonlar arasýndaki mesafeyi hesapla
            float touchDeltaMag = (touch0.position - touch1.position).magnitude;
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;   // Mesafedeki farký hesapla (zoomFactor) 
            Camera.main.orthographicSize += deltaMagnitudeDiff * zoomSpeed; // Kamera yakýnlaþtýrmasýný (ortografik veya perspektif) ayarla
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
        }
    }

    public void SetMovementEnabled(bool enabled)
    {
        isMovementEnabled = enabled; // Kamera hareket kontrolünü etkinleþtir veya devre dýþý býrak
    }
}
