using UnityEngine;
using TMPro;

public class DistrictView : MonoBehaviour
{
    public DistrictData data;
    public SpriteRenderer background;
    public TextMeshProUGUI nameText;

    public void Init(DistrictData district)
    {
        data = district;
        background.color = district.districtColor;
        nameText.color = district.districtColor;
        nameText.text = district.districtName;

        // Haritada otomatik konumla
        transform.position = new Vector3(district.coordinates.x, district.coordinates.y, district.coordinates.z);
    }
}
