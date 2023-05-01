using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "LevelList", menuName = "DataFile/LevelList", order = 2)]
    public class LevelList : ScriptableObject
    {
        [SerializeField] public Level[] _levels;
        public Level[] Levels => _levels;
    }
}