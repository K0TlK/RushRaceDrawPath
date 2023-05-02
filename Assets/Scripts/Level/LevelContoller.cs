using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RashLevel
{
    public class LevelContoller : MonoBehaviour
    {
        [SerializeField] private Marker[] _markers;
        public Marker[] Markers => _markers;

        [SerializeField] private FinalFlag[] _flags;
        public FinalFlag[] Flags => _flags;

        [SerializeField] private NavMeshSurface[] _surfaces;
        public NavMeshSurface[] Surfaces => _surfaces;

        [SerializeField] private LoseObject[] _loseObjects;
        public LoseObject[] LoseObjects => _loseObjects;

        public virtual bool Init()
        {
            _surfaces[0].BuildNavMesh();

            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;

            foreach (var marker in _markers)
            {
                marker.Init();
            }

            ResetLevel();

            return true;
        }

        public float GetMaxTime()
        {
            float max = 0;

            foreach (var marker in _markers)
            {
                if (max < marker.AnimTime)
                {
                    max = marker.AnimTime;
                }
            }

            return max;
        }

        public bool IsCompleteDraw()
        {
            foreach (var marker in _markers)
            {
                if (marker.IsMovable)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsFinal()
        {
            foreach (var marker in _markers)
            {
                if (!marker.IsFinished)
                {
                    return false;
                }
            }

            return true;
        }

        public void StartAnim()
        {
            foreach (var loseObject in _loseObjects)
            {
                loseObject.SetObjectState(true);
            }

            foreach (var marker in _markers)
            {
                marker.StartAnim();
            }
        }

        public void StopAnim()
        {
            foreach (var marker in _markers)
            {
                marker.StopAnim();
            }
        }

        public void ResetLevel()
        {
            foreach (var marker in _markers)
            {
                marker.ResetMarker();
            }

            foreach (var flag in _flags)
            {
                flag.ResetFlag();
            }

            foreach (var loseObject in _loseObjects)
            {
                loseObject.SetObjectState(false);
            }
        }
    }
}