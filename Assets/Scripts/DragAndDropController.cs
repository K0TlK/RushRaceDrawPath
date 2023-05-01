using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _sensetive = 1.0f;
    private Marker _selectedObject;
    private Vector3 _prevMousePos = Vector3.zero;
    

    void Start()
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
        }
        _prevMousePos = Input.mousePosition;
    }

    void Update()
    {
        if (_selectedObject == null && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent<Marker>(out Marker other))
                {
                    _selectedObject = other;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_selectedObject != null)
            {
                _selectedObject = null;
            }
        }

        if (_selectedObject != null)
        {
            Vector3 deltaPos = Input.mousePosition - _prevMousePos;
            deltaPos.z = deltaPos.y;
            deltaPos.y = 0;
            deltaPos /= Screen.height;
            _selectedObject.SetPos(deltaPos * _mainCamera.orthographicSize * 2 * _sensetive);
        }

        _prevMousePos = Input.mousePosition;
    }
}
