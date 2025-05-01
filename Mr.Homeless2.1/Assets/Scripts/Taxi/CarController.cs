using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    [Header("Input Settings")]
    public InputActionAsset InputActions;
    private InputAction moveAction;
    private InputAction handbrakeAction;

    [Header("Car Settings")]
    public float acceleration = 10f;
    public float maxSpeed = 20f;
    public float reverseSpeed = 10f;
    public float turnSpeed = 5f;
    public float driftFactor = 0.95f;
    public float brakeForce = 10f;
    public float handbrakeDriftFactor = 0.5f;

    private void Awake()
    {
        // Initialize input actions
        moveAction = InputActions.FindAction("CarController/Move");
        handbrakeAction = InputActions.FindAction("CarController/Handbrake");

    }

    private void OnEnable()
    {
        // Enable input actions
        InputActions.FindActionMap("CarController").Enable();
    }

    private void OnDisable()
    {
        // Disable input actions
        InputActions.FindActionMap("CarController").Disable();
    }



}


