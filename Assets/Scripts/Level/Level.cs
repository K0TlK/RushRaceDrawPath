using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{

    [CreateAssetMenu(fileName = "Level", menuName = "DataFile/Level", order = 1)]
    public class Level : ScriptableObject
    {
        public LevelContoller LevelPrefab;
        public string LevelName;
    }
}
