using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

namespace Level
{
    public class LevelContoller : MonoBehaviour
    {
        [SerializeField] private List<Marker> _markers = new List<Marker>();
        public List<Marker> Markers => _markers;

        [SerializeField] private List<FinalFlag> _flags = new List<FinalFlag>();
        private List<FinalFlag> Flags => _flags;

        public virtual bool Init()
        {
            foreach (var marker in _markers)
            {
                marker.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            }
            ResetLevel();
            NavMeshBuilder.ClearAllNavMeshes();
            NavMeshBuilder.BuildNavMesh();
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;

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