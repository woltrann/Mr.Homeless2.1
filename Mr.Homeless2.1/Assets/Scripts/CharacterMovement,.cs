using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public static CharacterMovement Instance;

    public float moveSpeed = 5f;
    private Transform target;

    void Awake() => Instance = this;

    void Update()
    {
        if (target != null)     //karakteri týklanan binaya doðru hareket eder
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            // Ýsteðe baðlý: hedefe ulaþýnca durdurmak istersen
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                BuildingUI.Instance.BuildingPanel2Show();
                target = null;
            }
        }
    }

    public void MoveTo(Transform newTarget)
    {
        target = newTarget;
    }
}
