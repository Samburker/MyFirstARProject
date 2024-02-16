using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class touchInput : MonoBehaviour
{
    [SerializeField] private TMP_Text debugText;
    [SerializeField] private GameObject ballPrefab;
    private ARRaycastManager arrcm;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    TrackableType trackableTypes = TrackableType.PlaneWithinPolygon;

    private void Start()
    {
        arrcm = GetComponent<ARRaycastManager>();
    }

    public void SingleTap(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            var touchPos = ctx.ReadValue<Vector2>();
            debugText.text = touchPos.ToString();

            if (arrcm.Raycast(touchPos, hits, trackableTypes))
            {
                var ball = Instantiate(ballPrefab, hits[0].pose.position, new Quaternion());
            }

        }
    }


}