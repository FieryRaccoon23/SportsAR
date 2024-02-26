using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Stadium;

    UnityEvent m_PlacementUpdate;

    ARRaycastManager m_RaycastManager;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    private GameObject m_SpawnedObject;

    GameControls m_GameControls;

    bool m_IsPressed = false;

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();

        if (m_PlacementUpdate == null)
            m_PlacementUpdate = new UnityEvent();

        m_GameControls = new GameControls();

        m_GameControls.Controls.Touch.performed += _ => m_IsPressed = true;
        m_GameControls.Controls.Touch.canceled += _ => m_IsPressed = false;
        //m_PlacementUpdate.AddListener(DiableVisual);
    }

    // TODO: use new touch input
    //bool TryGetTouchPosition(out Vector2 touchPosition)
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        touchPosition = Input.GetTouch(0).position;
    //        return true;
    //    }

    //    touchPosition = default;
    //    return false;
    //}

    private void OnEnable()
    {
        m_GameControls.Controls.Enable();
    }


    private void OnDisable()
    {
        m_GameControls.Controls.Disable();
    }

    void Update()
    {
        if (Pointer.current == null || !m_IsPressed)
            return;

        var touchPosition = Pointer.current.position.ReadValue();

        //if (!TryGetTouchPosition(out Vector2 touchPosition))
        //    return;

        if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;

            if (m_SpawnedObject == null)
            {
                m_SpawnedObject = Instantiate(m_Stadium, hitPose.position, hitPose.rotation);

            }
            else
            {
                m_SpawnedObject.transform.position = hitPose.position;
            }
            //placementUpdate.Invoke();
        }
    }
}
