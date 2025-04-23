using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset InputActions;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minZoomDistance = 5f;
    [SerializeField] private float maxZoomDistance = 20f;
    [SerializeField] private float zoomSmoothTime = 0.2f;
    private float zoomVelocity;

    private Rigidbody rb;
    private InputAction moveAction;
    private InputAction turnAction;
    private InputAction zoomAction;

    private Vector2 moveInput;
    private float turnInput;
    private float zoomInput;

    private void OnEnable()
    {
        InputActions.FindActionMap("MapController").Enable();
    }
    private void OnDisable()
    {
        InputActions.FindActionMap("MapController").Disable();
    }
    private void Awake()
    {
        moveAction = InputActions.FindAction("MapController/Move");
        turnAction = InputActions.FindAction("MapController/Turn");
        zoomAction = InputActions.FindAction("MapController/Zoom");

        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        turnInput = turnAction.ReadValue<float>();
        zoomInput = zoomAction.ReadValue<float>();

        Transform cam = Camera.main.transform;

        Move(cam);
        Turn();
        Zoom(cam);
    }


    private void Move(Transform cam)
    {
        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = cam.right;
        camRight.y = 0;
        camRight.Normalize();
        Vector3 moveDirection = camForward * moveInput.y + camRight * moveInput.x;
        moveDirection.Normalize();
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    private void Turn()
    {
        Quaternion turnRotation = Quaternion.Euler(0, turnInput * turnSpeed * Time.deltaTime, 0);
        rb.MoveRotation(rb.rotation * turnRotation);
    }
    
    private void Zoom(Transform cam)
    {
        Vector3 direction = (cam.position - transform.position).normalized;
        float currentDistance = Vector3.Distance(cam.position, transform.position);
        float targetDistance = Mathf.Clamp(currentDistance - zoomInput * zoomSpeed, minZoomDistance, maxZoomDistance);

        float smoothedDistance = Mathf.SmoothDamp(currentDistance, targetDistance, ref zoomVelocity, zoomSmoothTime);
        cam.position = transform.position + direction * smoothedDistance;
    }

}
