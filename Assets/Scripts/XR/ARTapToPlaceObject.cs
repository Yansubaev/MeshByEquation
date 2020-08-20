using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObject : MonoBehaviour
{

    [SerializeField]
    private GameObject _goToInstantiate;

    private GameObject _spawnedObject;
    private ARRaycastManager _aRRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool _move = true;

    public event Action OnObjectPlaced = delegate { };

    private void Awake()
    {
        _aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if(_aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon) && _move)
        {
            var hitPose = hits[0].pose;
            _goToInstantiate.transform.position = hitPose.position;
            _goToInstantiate.SetActive(true);
            _move = false;
        }
    }

    public void InstantiateGO()
    {
        _spawnedObject = Instantiate(_goToInstantiate, new Vector3(1, 12, 3), Quaternion.Euler(0, 90, 0));
    }

    public void Restart()
    {
        _move = true;
        _goToInstantiate.SetActive(false);
    }
}
