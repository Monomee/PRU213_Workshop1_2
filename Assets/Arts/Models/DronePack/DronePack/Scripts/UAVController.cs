using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class UAVController : MonoBehaviour
{
    [Header("Movement Physics")]
    [SerializeField] private float moveForce = 15f;
    [SerializeField] private float rotationTorque = 5f;
    [SerializeField] private float dragBase = 2f; 

    [Header("Visual Settings")]
    [SerializeField] private Transform visualModel;
    [SerializeField] private float tiltAngle = 15f;
    [SerializeField] private float tiltSmoothness = 5f;

    [Header("Input Actions")]
    [SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction rotateAction;

    private Rigidbody rb;
    private Vector3 moveInput;
    private float rotateInput;

    [SerializeField] private FanRotation[] fans;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.drag = dragBase;
        rb.angularDrag = dragBase;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        rotateAction.Enable();
        SetActiveFans(true);
    }

    private void OnDisable()
    {
        moveAction.Disable();
        rotateAction.Disable();
    }

    private void Update()
    {
        moveInput = moveAction.ReadValue<Vector3>();
        rotateInput = rotateAction.ReadValue<float>();

        HandleVisualTilt();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        Vector3 forceDirection = transform.right * moveInput.x +
                                 transform.up * moveInput.y +
                                 transform.forward * moveInput.z;

        rb.AddForce(forceDirection * moveForce, ForceMode.Acceleration);

        if (Mathf.Abs(rotateInput) > 0.1f)
        {
            rb.AddTorque(Vector3.up * rotateInput * rotationTorque, ForceMode.Acceleration);
        }
        else
        {
            Vector3 currentAngularVelocity = rb.angularVelocity;
            currentAngularVelocity.y = 0;
            rb.angularVelocity = currentAngularVelocity;
        }
    }

    private void HandleVisualTilt()
    {
        if (visualModel == null) return;

        float targetTiltX = moveInput.z * tiltAngle;
        float targetTiltZ = -moveInput.x * tiltAngle;

        Quaternion targetRotation = Quaternion.Euler(targetTiltX, 0, targetTiltZ);
        visualModel.localRotation = Quaternion.Slerp(visualModel.localRotation, targetRotation, Time.deltaTime * tiltSmoothness);
    }

    private void SetActiveFans(bool state)
    {
        if (fans == null) return;
        foreach (var fan in fans)
        {
            if (fan != null) fan.isActive = state;
        }
    }
}