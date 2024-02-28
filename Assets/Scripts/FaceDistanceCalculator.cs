using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;
using UnityEngine.XR.ARSubsystems;

public class FaceDistanceCalculator : MonoBehaviour
{
    public ARFaceManager faceManager; // Reference to ARFaceManager
    public TextMeshProUGUI distanceText; // Reference to TextMeshPro text object

    private ARFace trackedFace; // Reference to tracked ARFace component

    private Vector3 vertex1;
    private Vector3 vertex2;

    void Update()
    {
        // Get the first tracked face from the ARFaceManager
        trackedFace = GetTrackedFace();

        // Ensure the face is valid and being tracked
        if (trackedFace != null && trackedFace.trackingState == TrackingState.Tracking)
        {
            // Example: Select vertices 0 and 100 for demonstration
            // suu kiinni unit 0,02 ja suu täysin auki 0,06 units 
            vertex1 = trackedFace.vertices[0];
            vertex2 = trackedFace.vertices[15];

            // Calculate the distance between the two vertices
            float distance = Vector3.Distance(vertex1, vertex2);

            // Update the text to display the distance
            distanceText.text = "Distance: " + distance.ToString("F2") + " units";
        }
        else
        {
            // If the face is not being tracked, display a message
            distanceText.text = "Face not detected";
        }
    }

    ARFace GetTrackedFace()
    {
        foreach (var face in faceManager.trackables)
        {
            if (face.trackingState == TrackingState.Tracking)
                return face;
        }
        return null;
    }
}
