using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

public class BirdSpawner : MonoBehaviour
{
    public GameObject robinPrefab;
    public GameObject blueJayPrefab;

    ARTrackedImageManager trackedImageManager;

    void Start()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            SpawnBird(trackedImage);
        }
    }

    void SpawnBird(ARTrackedImage trackedImage)
    {
        GameObject prefabToSpawn = null;

        if (trackedImage.referenceImage.name == "RobinBirdImage")
        {
            prefabToSpawn = robinPrefab;
        }
        else if (trackedImage.referenceImage.name == "BlueJayBirdImage")
        {
            prefabToSpawn = blueJayPrefab;
        }

        if (prefabToSpawn != null)
        {
            Instantiate(prefabToSpawn, trackedImage.transform.position, trackedImage.transform.rotation, trackedImage.transform);
        }
    }
}
