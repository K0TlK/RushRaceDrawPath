using StarterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class LevelLoader : Singleton<LevelLoader>
    {
        public Level level;

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            StartLevel(level);
        }

        public void StartLevel(Level newLevel)
        {
            var levelPrefab = Instantiate(newLevel.LevelPrefab);
            GameManager.Instance.SetLevel(levelPrefab);
            levelPrefab.Init();
            Debug.Log($"Load Level: {newLevel.LevelName}.");
        }
    }
}