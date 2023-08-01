using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _sensetive = 1.0f;
    [SerializeField] private LayerMask _layerMask;

    private Marker _selectedObject;
    private Vector3 _prevMousePos;
    

    void Awake()
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

            if (Physics.Raycast(ray, out hit, 20.0f, _layerMask))
            {
                if (hit.collider.TryGetComponent<Marker>(out Marker other))
                {
                    _selectedObject = other;
                    _selectedObject.Select();
                }
            }

            _prevMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_selectedObject != null)
            {
                _selectedObject.Deselect();
                _selectedObject = null;
                _prevMousePos = Vector3.zero;
            }
        }

        if (_selectedObject != null && _prevMousePos != Vector3.zero)
        {
            Vector3 deltaPos = Input.mousePosition - _prevMousePos;
            deltaPos.z = deltaPos.y;
            deltaPos.y = 0;
            deltaPos /= Screen.height;
            _selectedObject.SetPos(deltaPos * _mainCamera.orthographicSize * 2 * _sensetive);
            _prevMousePos = Input.mousePosition;
        }
    }
}
