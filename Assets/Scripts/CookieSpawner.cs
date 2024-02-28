using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CookieSpawner : MonoBehaviour
{
    public ARFaceManager faceManager; // Reference to ARFaceManager
    public GameObject cookiePrefab; // Reference to the cookie prefab
    public AudioClip munchSound; // Munching sound effect

    public float mouthOpenDistance = 0.04f; // Distance indicating mouth is open
    public float mouthClosedDistance = 0.02f; // Distance indicating mouth is closed

    public int cookieAttachVertexIndex = 0; // Vertex index to which the cookie is attached

    private ARFace trackedFace; // Reference to tracked ARFace component
    private GameObject spawnedCookie; // Reference to the spawned cookie
    private CookieGameManager cookieGameManager; // Reference to CookieGameManager script

    private bool mouthOpen = false; // Flag indicating whether the mouth is open

    void Start()
    {
        // Find the CookieGameManager script in the scene
        cookieGameManager = FindObjectOfType<CookieGameManager>();
    }

    void Update()
    {
        // Get the first tracked face from the ARFaceManager
        trackedFace = GetTrackedFace();

        // Ensure the face is valid and being tracked
        if (trackedFace != null && trackedFace.trackingState == TrackingState.Tracking)
        {
            // Calculate the distance between vertex 0 and vertex 15
            Vector3 vertex0 = trackedFace.vertices[0];
            Vector3 vertex15 = trackedFace.vertices[15];
            float distance = Vector3.Distance(vertex0, vertex15);

            // Check if the mouth is open based on distance
            if (distance > mouthOpenDistance)
            {
                // Spawn the cookie if it hasn't been spawned yet
                if (!mouthOpen)
                {
                    AttachCookieToFace(trackedFace, cookieAttachVertexIndex);
                    mouthOpen = true;
                    // Increment cookie counter when a cookie is spawned
                }
            }
            else
            {
                // Destroy the cookie if it exists
                if (mouthOpen)
                {
                    DestroyCookie();
                    cookieGameManager.IncrementCookieCounter();

                    mouthOpen = false;
                }
            }
        }
    }

    void AttachCookieToFace(ARFace face, int vertexIndex)
    {
        // Disable the default face mesh visualization
        ARFaceMeshVisualizer visualizer = face.GetComponent<ARFaceMeshVisualizer>();
        if (visualizer != null)
            visualizer.enabled = false;

        // Check if the vertex index is within the range of vertices array
        if (vertexIndex < 0 || vertexIndex >= face.vertices.Length)
        {
            Debug.LogWarning("Invalid vertex index. Make sure it's within the range of vertices array.");
            return;
        }

        // Get the position of the specified vertex
        Vector3 vertexPosition = face.transform.TransformPoint(face.vertices[vertexIndex]);

        // Instantiate the cookie and attach it to the face
        spawnedCookie = Instantiate(cookiePrefab, vertexPosition, Quaternion.identity, face.transform);
        spawnedCookie.transform.rotation = face.transform.rotation;
    }

    void DestroyCookie()
    {
        // Play munch sound effect
        if (munchSound != null)
        {
            AudioSource.PlayClipAtPoint(munchSound, spawnedCookie.transform.position);
        }

        // Destroy the cookie
        Destroy(spawnedCookie);
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
