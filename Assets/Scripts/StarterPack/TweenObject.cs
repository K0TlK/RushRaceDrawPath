using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterPack
{
    public class TweenObject : MonoBehaviour
    {
        private DG.Tweening.Sequence _sequence;
        protected DG.Tweening.Sequence _tweenSequence => _sequence;

        protected void NewSequence() // TODO
        {
            if (_sequence != null)
            {
                _sequence.Kill();
            }
            _sequence = DOTween.Sequence();
        }

        private void OnDisable()
        {
            _sequence.Kill();
        }
    }
}