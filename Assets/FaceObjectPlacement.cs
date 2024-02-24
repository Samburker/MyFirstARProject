using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FaceObjectPlacement : MonoBehaviour
{
    public GameObject objectToAttach;
    public ARFaceManager faceManager;
    public int upperLipVertexIndex = 10; // Default value, can be adjusted in the Inspector

    void Start()
    {
        faceManager = GetComponent<ARFaceManager>();
        faceManager.facesChanged += OnFacesChanged;
    }

    void OnFacesChanged(ARFacesChangedEventArgs args)
    {
        foreach (var face in args.added)
        {
            AttachObjectToFace(face);
        }
    }

    void AttachObjectToFace(ARFace face)
    {
        if (face.trackingState != TrackingState.Tracking)
            return;

        // Check if the vertex index is within the range of vertices array
        if (upperLipVertexIndex < 0 || upperLipVertexIndex >= face.vertices.Length)
        {
            Debug.LogWarning("Invalid vertex index. Make sure it's within the range of vertices array.");
            return;
        }

        // Get the position of the upper lip
        Vector3 upperLipPosition = face.transform.TransformPoint(face.vertices[upperLipVertexIndex]);

        // Instantiate the object
        GameObject obj = Instantiate(objectToAttach, upperLipPosition, Quaternion.identity, face.transform);

        // Adjust object's rotation if needed
        //obj.transform.rotation = face.transform.rotation;

        // Optionally, adjust the scale of the object based on face size or other factors
        // obj.transform.localScale = Vector3.one * scaleFactor;
    }
}
