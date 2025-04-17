using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BuildingClickHandler : MonoBehaviour
{
    private bool isDragging = false;
    [SerializeField]
    private CameraPan cameraPan; // CameraPan referans�

    void Update()
    {
        if (Input.touchCount == 1) // Tek dokunma kontrol�
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isDragging = false; // Dokunma ba�lad���nda s�f�rla
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                isDragging = true; // Kayd�rma yap�l�yorsa i�aretle
            }
            else if (touch.phase == TouchPhase.Ended) // Dokunma bitti�inde kontrol et
            {
                if (!isDragging) // E�er kayd�rma yap�lmad�ysa bina t�klama kontrol�
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out RaycastHit hit)) // Raycast ile bina t�klamas� kontrol�
                    {
                        if (hit.collider.CompareTag("Building"))
                        {
                            //isDragging = true;
                            BuildingUIManager.Instance.OpenBuildingUI(hit.collider.gameObject);
                            Debug.Log("Bina t�kland� ve panel a��ld�!");
                            //isDragging = false;

                        }
                    }
                }
            }
        }
    }
}
