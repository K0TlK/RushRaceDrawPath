using DG.Tweening;
using StarterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.ParticleSystem;

public class Marker : TweenObject, DaDObject
{
    [SerializeField] private float _distanceBetweenPaths = 0.5f;
    [SerializeField] private float _animTime = 10.0f;
    public float AnimTime => _animTime;

    [SerializeField] private AnimationCurve _animCurve;

    [SerializeField] private LineRenderer _path;
    [SerializeField] private Material _sprite;
    [SerializeField] private Vector3 _lineOffset = Vector3.zero;
    [SerializeField] private NavMeshAgent _agent;

    private bool _isMovable = false;

    public bool isMovable()
    {
        return _isMovable;
    }

    private bool _isFinishied = false;

    public bool IsFinished => _isFinishied;

    private Vector3 _startPos = Vector3.zero;

    public void Init()
    {
        _startPos = transform.position;
        ActiveMarker();
    }

    public void ResetMarker()
    {
        _path.positionCount = 0;
        ActiveMarker();
        Deselect();
        transform.position = _startPos;
        _path.SetPosition(0, _startPos);
        _isFinishied = false;
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
        if (Vector3.Distance(_path.GetPosition(_path.positionCount - 1), trailPos) >= _distanceBetweenPaths)
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
        _isFinishied = false;
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
        _agent.enabled = false;
        Color newColor = _sprite.color;
        newColor.a = 0.4f;
        _sprite.color = newColor;
        _isMovable = false;
        _isFinishied = false;
    }

    public void Select()
    {
        _agent.enabled = true;
    }

    public void Deselect()
    {
        _agent.enabled = false;
    }

    public void StartAnim()
    {
        NewSequence();
        float distance = GetPathDistance();

        Color newColor = _sprite.color;
        newColor.a = 1;
        _sprite.color = newColor;

        _agent.enabled = false;

        _isMovable = false;

        Vector3 prevPos = _startPos;
        Vector3 nextPos;
        for (var i = 0; i < _path.positionCount; i++)
        {
            nextPos = _path.GetPosition(i);
            float partDistance = Vector3.Distance(prevPos, nextPos);
            _tweenSequence.Append(transform.DOMove(nextPos, _animTime * (partDistance / distance)).SetEase(_animCurve));
            prevPos = nextPos;
        }

        _tweenSequence.AppendCallback(() => { _isFinishied = true; });
    }

    public void StopAnim()
    {

        _tweenSequence.Kill();
    }

    public float GetPathDistance()
    {
        float distance = 0;

        Vector3 prevPos = _startPos;
        Vector3 nextPos;
        for (var i = 0; i < _path.positionCount; i++)
        {
            nextPos = _path.GetPosition(i);
            distance += Vector3.Distance(prevPos, nextPos);
            prevPos = nextPos;
        }

        return distance;
    }
}
