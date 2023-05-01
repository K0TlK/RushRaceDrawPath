using Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private int _index = -1;
    public void LoadLevel(int index)
    {
        _index = index;
        LoadLevel();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadLevel()
    {
        SceneManager.sceneLoaded += StartLevel;
        SceneManager.LoadScene(1);
    }

    private void StartLevel(Scene scene, LoadSceneMode mode)
    {
        LevelLoader.Instance.StartLevel(_index);
        SceneManager.sceneLoaded -= StartLevel;
    }
}
