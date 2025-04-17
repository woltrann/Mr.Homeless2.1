using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class BuildingClickHandler : MonoBehaviour
{
    private bool isDragging = false;
    [SerializeField]
    private CameraPan cameraPan; // CameraPan referansý

    void Update()
    {
        if (Input.touchCount == 1) // Tek dokunma kontrolü
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isDragging = false; // Dokunma baþladýðýnda sýfýrla
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                isDragging = true; // Kaydýrma yapýlýyorsa iþaretle
            }
            else if (touch.phase == TouchPhase.Ended) // Dokunma bittiðinde kontrol et
            {
                if (!isDragging) // Eðer kaydýrma yapýlmadýysa bina týklama kontrolü
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out RaycastHit hit)) // Raycast ile bina týklamasý kontrolü
                    {
                        if (hit.collider.CompareTag("Building"))
                        {
                            //isDragging = true;
                            BuildingUIManager.Instance.OpenBuildingUI(hit.collider.gameObject);
                            Debug.Log("Bina týklandý ve panel açýldý!");
                            //isDragging = false;

                        }
                    }
                }
            }
        }
    }
}
