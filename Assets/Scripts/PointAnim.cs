using DG.Tweening;
using StarterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAnim : TweenObject
{
    [SerializeField] private float _offset = 1.0f;
    [SerializeField] private float _animSpeed = 1.0f;

    private Vector3 _startPos = Vector3.zero;

    private void Awake()
    {
        _startPos = transform.localPosition;
    }

    void OnEnable()
    {
        Vector3 newPos = _startPos;
        newPos.y += _offset;

        NewSequence();
        _tweenSequence.Append(transform.DOLocalMove(newPos, _animSpeed / 2))
                      .Append(transform.DOLocalMove(_startPos, _animSpeed / 2))
                      .SetLoops(-1);
    }

    private void OnDisable()
    {
        _tweenSequence.Kill();
    }
}
