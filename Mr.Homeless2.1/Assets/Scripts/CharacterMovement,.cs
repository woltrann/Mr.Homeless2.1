using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public static CharacterMovement Instance;

    public float moveSpeed = 5f;
    private Transform target;

    void Awake() => Instance = this;

    void Update()
    {
        if (target != null)     //karakteri t�klanan binaya do�ru hareket eder
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            // �ste�e ba�l�: hedefe ula��nca durdurmak istersen
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
