using UnityEngine;

public class CarMotor : MonoBehaviour
{
    public CarController CarController;

    private Rigidbody rb;
    private float acceleration;
    private float maxSpeed;
    private float reverseSpeed;
    private float turnSpeed;
    private float brakeForce;
    

    private Vector2 moveInput;

    private void Awake()
    {
        CarController = GetComponent<CarController>();

        // Initialize car settings
        rb = GetComponent<Rigidbody>();
        acceleration = CarController.acceleration;
        maxSpeed = CarController.maxSpeed;
        reverseSpeed = CarController.reverseSpeed;
        turnSpeed = CarController.turnSpeed;
        brakeForce = CarController.brakeForce;

        
    }
    private void FixedUpdate()
    {
        moveInput = CarController.InputActions.FindAction("CarController/Move").ReadValue<Vector2>();

        // Apply movement and drift mechanics
        ApplyAcceleration();
        ApplySteering();
        ApplyBraking();
    }

    private void ApplyAcceleration()
    {
        if (moveInput.y > 0)
        {
            Vector3 forwardForce = transform.forward * moveInput.y * acceleration;
            rb.AddForce(forwardForce, ForceMode.Acceleration);
        }
        else if (moveInput.y < 0 && rb.linearVelocity.magnitude < 0.1f) // sadece duruyorken geri
        {
            Vector3 reverseForce = transform.forward * moveInput.y * reverseSpeed;
            rb.AddForce(reverseForce, ForceMode.Acceleration);
        }

        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, moveInput.y > 0 ? maxSpeed : reverseSpeed);
    }


    private void ApplyBraking()
    {
        // Apply braking force when pressing S (negative vertical input) and the car is moving forward
        if (moveInput.y < 0 && rb.linearVelocity.magnitude > 0.1f)
        {
            Vector3 brakeForceVector = -rb.linearVelocity.normalized * brakeForce;
            rb.AddForce(brakeForceVector, ForceMode.Acceleration);
        }
    }

    private void ApplySteering()
    {
        // Only allow steering when the car is moving
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            float turn = moveInput.x * turnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
            rb.MoveRotation(rb.rotation * turnRotation);
        }
    }
}
