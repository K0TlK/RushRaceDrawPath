using Level;
using StarterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private LevelContoller _activeLevel;

    public void SetLevel(LevelContoller activeLevel)
    {
        _activeLevel = activeLevel;
    }

    public void TestForLevelEnd()
    {
        if (_activeLevel.IsCompleteDraw())
        {
            _activeLevel.StartAnim();
        }
    }

    public void RestartLevel()
    {
        _activeLevel.ResetLevel();
    }
}
