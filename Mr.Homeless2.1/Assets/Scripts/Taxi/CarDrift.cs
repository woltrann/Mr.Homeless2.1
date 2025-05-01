using UnityEngine;

public class CarDrift : MonoBehaviour
{
    public CarController CarController;

    private float handbrakeDriftFactor;
    private float driftFactor;

    private bool isHandbraking;
    private Rigidbody rb;

    private void Awake()
    {
        CarController = GetComponent<CarController>();
        handbrakeDriftFactor = CarController.handbrakeDriftFactor;
        driftFactor = CarController.driftFactor;
    }
    private void FixedUpdate()
    {
        isHandbraking = CarController.InputActions.FindAction("CarController/Handbrake").ReadValue<float>() > 0;
        Drift();
    }


    private void Drift()
    {
        // Calculate drift effect
        Vector3 velocity = rb.linearVelocity;
        Vector3 forward = transform.forward;

        float forwardVelocity = Vector3.Dot(velocity, forward);
        Vector3 driftVelocity = velocity - forward * forwardVelocity;

        // Apply drift factor or handbrake drift factor
        float currentDriftFactor = isHandbraking ? handbrakeDriftFactor : driftFactor;
        rb.linearVelocity = forward * forwardVelocity + driftVelocity * currentDriftFactor;
    }
}
