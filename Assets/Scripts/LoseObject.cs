using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseObject : MonoBehaviour
{
    private bool _objectActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!_objectActive)
        {
            return;
        }

        if (other.gameObject.TryGetComponent(out Marker marker))
        {
            if (!GameManager.Instance.IsAnimActive)
            {
                return;
            }

            Debug.Log($"Marker is Lost. Marker: {marker.name}");
            GameManager.Instance.StopAnim(marker.transform.position);
        }
    }

    public void SetObjectState(bool objectActive)
    {
        _objectActive = objectActive;
    }
}
