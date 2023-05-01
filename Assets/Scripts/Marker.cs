using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.ParticleSystem;

public class Marker : MonoBehaviour
{
    [SerializeField] private LineRenderer _path;
    [SerializeField] private Material _sprite;
    [SerializeField] private float _distance;
    [SerializeField] private Vector3 _lineOffset = Vector3.zero;

    private bool _isMovable = false;
    public bool IsMovable => _isMovable;

    private Vector3 _startPos = Vector3.zero;

    public void ResetMarker()
    {
        _path.positionCount = 0;
        ActiveMarker();
        _startPos = transform.position;
    }

    public void SetPos(Vector3 deltaPos)
    {
        if (!_isMovable)
        {
            return;
        }

        if (deltaPos == Vector3.zero)
        {
            return;
        }

        transform.position += deltaPos;
        Vector3 trailPos = transform.position + _lineOffset;
        if (Vector3.Distance(_path.GetPosition(_path.positionCount - 1), trailPos) >= _distance)
        {
            _path.positionCount++;
            _path.SetPosition(_path.positionCount - 1, trailPos);
        }
    }

    public void ActiveMarker()
    {
        Color newColor = _sprite.color;
        newColor.a = 1;
        _sprite.color = newColor;
        _isMovable = true;
        _path.positionCount = 0;
        _path.positionCount++;
        _path.SetPosition(_path.positionCount - 1, transform.position + _lineOffset);
    }

    public void DeactiveMarker(Vector3 finalPos)
    {
        _path.positionCount++;
        Vector3 prevPos = _path.GetPosition(_path.positionCount - 1);
        finalPos.y = prevPos.y;
        prevPos -= _lineOffset;
        _path.SetPosition(_path.positionCount - 1, finalPos + _lineOffset);
        transform.position = _startPos;
        DeactiveMarker();
    }

    public void DeactiveMarker()
    {
        Color newColor = _sprite.color;
        newColor.a = 0.4f;
        _sprite.color = newColor;
        _isMovable = false;
        GameManager.Instance.TestForLevelEnd();
    }
}
