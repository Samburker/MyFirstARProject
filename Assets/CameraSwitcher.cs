using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CameraSwitcher : MonoBehaviour
{
    public ARCameraManager arCameraManager;

    private float lastTapTime;

    void Start()
    {
        // Find the ARCameraManager component if not set in the inspector
        if (arCameraManager == null)
        {
            arCameraManager = FindObjectOfType<ARCameraManager>();
        }

        lastTapTime = Time.unscaledTime;
    }

    void Update()
    {
        // Check for double tap
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            float currentTime = Time.unscaledTime;
            if (currentTime - lastTapTime < 0.3f)
            {
                ToggleCamera();
            }
            lastTapTime = currentTime;
        }
    }

    public void ToggleCamera()
    {
        if (arCameraManager == null)
        {
            Debug.LogError("ARCameraManager is not assigned.");
            return;
        }

        // Toggle between World and User facing directions
        arCameraManager.requestedFacingDirection =
            (arCameraManager.requestedFacingDirection == CameraFacingDirection.World) ?
            CameraFacingDirection.User :
            CameraFacingDirection.World;
    }
}
