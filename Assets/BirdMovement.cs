using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed
    public float turnSpeed = 100f; // Adjust the turn speed as needed
    public float maxPitchAngle = 45f; // Maximum angle for pitching (looking up or down)

    private JoysticScript joystick;

    void Start()
    {
        // Find the Joystick script attached to the joystick handle
        joystick = FindObjectOfType<JoysticScript>();
    }

    void Update()
    {
        // Get input direction from the joystick
        Vector2 inputDirection = joystick.GetInputDirection();

        // Calculate horizontal movement based on input direction
        float horizontalMovement = inputDirection.x * turnSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, horizontalMovement);

        // Calculate vertical movement based on input direction
        float verticalMovement = inputDirection.y * moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * verticalMovement, Space.Self);

        // Limit pitch angle
        float currentPitch = transform.rotation.eulerAngles.x;
        float clampedPitch = Mathf.Clamp(currentPitch, -maxPitchAngle, maxPitchAngle);
        transform.rotation = Quaternion.Euler(clampedPitch, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
