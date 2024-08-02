using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class XRRigMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f; // Default movement speed
    public float gravity = -9.81f; // Gravity value

    private CharacterController characterController;
    private Vector3 movementDirection;
    private InputDevice leftController;
    private InputDevice rightController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Initialize the controllers
        InitializeControllers();
    }

    void Update()
    {
        // Check if controllers are valid, reinitialize if necessary
        if (!leftController.isValid || !rightController.isValid)
        {
            InitializeControllers();
        }

        // Get input from controllers
        Vector2 leftInput = Vector2.zero;
        Vector2 rightInput = Vector2.zero;

        if (leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftInput))
        {
            // Use left input if needed
        }

        if (rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out rightInput))
        {
            // Use right input for movement
            Vector3 forward = transform.forward * rightInput.y;
            Vector3 right = transform.right * rightInput.x;
            movementDirection = (forward + right).normalized;
        }

        // Apply gravity
        if (!characterController.isGrounded)
        {
            movementDirection.y += gravity * Time.deltaTime;
        }

        // Move the character controller
        characterController.Move(movementDirection * movementSpeed * Time.deltaTime);
    }

    void InitializeControllers()
    {
        var leftHandedDevices = new List<InputDevice>();
        var rightHandedDevices = new List<InputDevice>();

        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandedDevices);
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandedDevices);

        if (leftHandedDevices.Count > 0)
        {
            leftController = leftHandedDevices[0];
        }

        if (rightHandedDevices.Count > 0)
        {
            rightController = rightHandedDevices[0];
        }
    }

    public void IncreaseSpeed(float increment)
    {
        movementSpeed += increment;
    }

    public void SetSpeed(float newSpeed)
    {
        movementSpeed = newSpeed;
    }
}
