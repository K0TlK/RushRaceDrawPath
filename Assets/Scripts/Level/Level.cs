using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{

    [CreateAssetMenu(fileName = "Level", menuName = "DataFile/Level", order = 1)]
    public class Level : ScriptableObject
    {
        [SerializeField] private LevelContoller _levelPrefab;
        public LevelContoller LevelPrefab => _levelPrefab;

        [SerializeField] private string _levelName;
        public string LevelName => _levelName;
    }
}
