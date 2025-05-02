using UnityEngine;

public class Building : MonoBehaviour
{
    public string buildingName = "Ýsimsiz Bina";
    public static Building LastVisitedBuilding; // Son ziyaret edilen bina
    public BuildingUI timeAdd;

    void OnMouseDown()      // Git butonuna basýnca çalýþan fonksiyon
    {
        float travelTime = 0f;
        if (LastVisitedBuilding != null && LastVisitedBuilding != this)
        {
            float distance = Vector3.Distance(transform.position, LastVisitedBuilding.transform.position);
            if (distance > 0 && distance <= 10f)
                travelTime = 30f; // 30 dakika
            else if (distance > 10 && distance <= 20f)
                travelTime = 60f; // 60 dakika
            else if (distance > 20 && distance <= 30f)
                travelTime = 90f; // 1.5 saat
            else
                travelTime = 120f; // 2 saat
        }
        //else if (LastVisitedBuilding = this) { BuildingUI.Instance.BuildingPanel2Show(); }

        BuildingUI.Instance.Show(buildingName, travelTime, () =>   
        { 
            Debug.Log($"{buildingName} için iþlem baþlatýldý!");
            BuildingUI.Instance.Clock(travelTime);
            CharacterMovement.Instance.MoveTo(transform);            
            LastVisitedBuilding = this; // Þu anki binayý "son gidilen" olarak kaydet
        }); 
    }
}
