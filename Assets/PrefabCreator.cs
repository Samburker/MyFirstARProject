using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PrefabCreator : MonoBehaviour
{
    [SerializeField] private GameObject robinPrefab;
    [SerializeField] private GameObject blueJayPrefab;
    [SerializeField] private Vector3 prefabOffset;

    private GameObject blueJay;
    private GameObject robin;
    private ARTrackedImageManager aRTrackedImageManager;

    private void OnEnable()
    {
        aRTrackedImageManager = gameObject.GetComponent<ARTrackedImageManager>();
        aRTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        foreach(ARTrackedImage image in obj.added)
        {
            blueJay = Instantiate(blueJayPrefab, image.transform);
            blueJay.transform.position += prefabOffset;

        }
    }
}
