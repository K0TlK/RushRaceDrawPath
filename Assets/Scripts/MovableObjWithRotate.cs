using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObjWithRotate : MonoBehaviour
{
    [SerializeField] private List<Transform> _pos;
    private List<Vector3> _posFinal = new List<Vector3>();
    [SerializeField] private float _animTime = 30.0f;
    [SerializeField] private AnimationCurve _movementCurve;
    [SerializeField] private bool _isNeedRotate = true;
    private Sequence _sequence;

    private void Awake()
    {
        for (var i = 0; i < _pos.Count; i++)
        {
            _posFinal.Add(_pos[i].position);
            Destroy(_pos[i].gameObject);
        }

        _pos.Clear();
    }

    void OnEnable()
    {
        _sequence = DOTween.Sequence();

        List<float> distance = new List<float>();
        float sumDistance = 0;

        for (var i = 0; i < _posFinal.Count - 1; i++)
        {
            distance.Add(Vector3.Distance(_posFinal[i], _posFinal[i + 1]));
            sumDistance += distance[distance.Count - 1];
        }
        distance.Add(Vector3.Distance(_posFinal[_posFinal.Count - 1], _posFinal[0]));
        sumDistance += distance[distance.Count - 1];


        transform.position = _posFinal[0];
        Quaternion prevRotation = Quaternion.LookRotation(_posFinal[0] - _posFinal[_posFinal.Count - 1], Vector3.up);
        if (_isNeedRotate)
        {
            transform.rotation = prevRotation;
        }

        for (var i = 1; i < _posFinal.Count; i++)
        {
            float partTime = (distance[i - 1] / sumDistance) * _animTime;
            _sequence.Append(transform.DOMove(_posFinal[i], partTime).SetEase(_movementCurve));
            if (_isNeedRotate)
            {
                prevRotation = Quaternion.LookRotation(_posFinal[i] - _posFinal[i - 1], Vector3.up);
                _sequence.Join(transform.DORotate(prevRotation.eulerAngles, partTime).SetEase(_movementCurve));
            }
        }

        _sequence.Append(transform.DOMove(_posFinal[0], (distance[distance.Count - 1] / sumDistance) * _animTime).SetEase(_movementCurve));
        if (_isNeedRotate)
        {
            prevRotation = Quaternion.LookRotation(_posFinal[0] - _posFinal[_posFinal.Count - 1], Vector3.up);
            _sequence.Join(transform.DORotate(prevRotation.eulerAngles, (distance[distance.Count - 1] / sumDistance) * _animTime).SetEase(_movementCurve));
        }

        _sequence.SetLoops(-1);
    }

    private void OnDisable()
    {
        _sequence.Kill();
    }
}
