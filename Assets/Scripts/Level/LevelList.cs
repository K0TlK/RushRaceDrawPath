using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "LevelList", menuName = "DataFile/LevelList", order = 2)]
    public class LevelList : ScriptableObject
    {
        public List<Level> levels = new List<Level>();
    }
}