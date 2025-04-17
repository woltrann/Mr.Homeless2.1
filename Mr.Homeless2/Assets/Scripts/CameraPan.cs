using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public float zoomSpeed = 0.01f;            // Yak�nla�t�rma h�z�
    public float minZoom = 5f;                // Minimum yak�nla�t�rma seviyesi
    public float maxZoom = 20f;               // Maksimum uzakla�t�rma seviyesi
    public float panSpeed = 0.05f;            // Kameran�n hareket h�z�
    public Vector2 panLimitMin;               // Kameran�n gidebilece�i minimum s�n�r
    public Vector2 panLimitMax;               // Kameran�n gidebilece�i maksimum s�n�r

    private Vector3 lastPosition;
    private bool isDragging = false;          // Kayd�rma yap�l�p yap�lmad���n� kontrol etmek i�in
    private bool isMovementEnabled = true;    // Kameran�n hareket edip etmeyece�ini kontrol eden de�i�ken

    public static CameraPan Instance { get; private set; } // Singleton Instance

    void Start()
    {
        if (Instance == null)
        {
            Instance = this; // Singleton atamas� yap�l�yor
        }
        else
        {
            Debug.LogError("Birden fazla CameraPan Instance'i var!");
            Destroy(this);
        }
    }

    void Update()
    {
        if (!isMovementEnabled) return; // E�er kamera hareketi devre d���ysa hi�bir �ey yapma

        if (Input.touchCount == 1) // Tek dokunma: Kamera hareketi veya bina t�klamas�
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isDragging = false; // Kayd�rma ba�lang�c�nda s�f�rla

                lastPosition = Camera.main.ScreenToWorldPoint(touch.position);      // Kamera hareketi i�in pozisyon kayd�
                lastPosition.y = 0;         // Sadece x ve z ekseninde hareket edelim
            }
            else if (touch.phase == TouchPhase.Moved) // Kamera hareketi
            {
                isDragging = true; // Hareket ediliyorsa kayd�rma yap�ld���n� i�aretle

                Vector3 deltaPosition = new Vector3(touch.deltaPosition.x, 0, touch.deltaPosition.y) * panSpeed;
                Vector3 newPosition = transform.position - deltaPosition; // Eksi koyduk ��nk� hareket tersine �al���yor
                newPosition.x = Mathf.Clamp(newPosition.x, panLimitMin.x, panLimitMax.x);
                newPosition.z = Mathf.Clamp(newPosition.z, panLimitMin.y, panLimitMax.y);
                transform.position = newPosition;
            }
        }
        if (Input.touchCount == 2)  // E�er 2 dokunma varsa 
        {
            isDragging = true;
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            float prevTouchDeltaMag = (touch0.position - touch0.deltaPosition - (touch1.position - touch1.deltaPosition)).magnitude;    // �nceki ve �imdiki pozisyonlar aras�ndaki mesafeyi hesapla
            float touchDeltaMag = (touch0.position - touch1.position).magnitude;
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;   // Mesafedeki fark� hesapla (zoomFactor) 
            Camera.main.orthographicSize += deltaMagnitudeDiff * zoomSpeed; // Kamera yak�nla�t�rmas�n� (ortografik veya perspektif) ayarla
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, minZoom, maxZoom);
        }
    }

    public void SetMovementEnabled(bool enabled)
    {
        isMovementEnabled = enabled; // Kamera hareket kontrol�n� etkinle�tir veya devre d��� b�rak
    }
}
