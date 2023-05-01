using StarterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class LevelLoader : Singleton<LevelLoader>
    {
        [SerializeField] private LevelList levelList;
        private int _levelIndex;

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public void NextLevel()
        {
            _levelIndex++;
            if (_levelIndex >= levelList.Levels.Length)
            {
                _levelIndex--;
            }

            StartLevel(_levelIndex);
        }

        public void PrevLevel()
        {
            _levelIndex--;
            if (_levelIndex < 0)
            {
                _levelIndex++;
            }

            StartLevel(_levelIndex);
        }

        public void StartLevel(int levelIndex)
        {
            _levelIndex = levelIndex;
            StartLevel(levelList.Levels[levelIndex]);
        }

        public void StartLevel(Level newLevel)
        {
            var levelPrefab = Instantiate(newLevel.LevelPrefab);
            GameManager.Instance.SetLevel(levelPrefab);
            levelPrefab.Init();
            Debug.Log($"<i> Load Level: {newLevel.LevelName}.");
        }
    }
}