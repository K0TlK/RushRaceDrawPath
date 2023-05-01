using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Level
{
    public class LevelContoller : MonoBehaviour
    {
        [SerializeField] private List<Marker> _markers = new List<Marker>();
        public List<Marker> Markers => _markers;

        [SerializeField] private List<FinalFlag> _flags = new List<FinalFlag>();
        public List<FinalFlag> Flags => _flags;

        [SerializeField] private List<NavMeshSurface> _surfaces = new List<NavMeshSurface>();
        public List<NavMeshSurface> Surfaces => _surfaces;

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

        public void StartAnim()
        {
            foreach (var marker in _markers)
            {
                marker.StartAnim();
            }
        }

        public void StopAnim()
        {

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
        }
    }
}