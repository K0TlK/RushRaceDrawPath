using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class FinalFlag : MonoBehaviour
{
    [SerializeField] private List<Marker> _correctMarkers = new List<Marker>();
    private bool _isFinished = false;

    public void ResetFlag()
    {
        _isFinished = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isFinished)
        {
            return;
        }

        if (other.gameObject.TryGetComponent(out Marker marker))
        {
            if (_correctMarkers.Contains(marker))
            {
                _isFinished = true;
                marker.DeactiveMarker(transform.position);
            }
        }
    }
}
