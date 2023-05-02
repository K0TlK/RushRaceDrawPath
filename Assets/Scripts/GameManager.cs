using RashLevel;
using StarterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : Singleton<GameManager>
{
    private LevelContoller _activeLevel;
    private bool _isAnimActive = false;
    public bool IsAnimActive => _isAnimActive;

    public void SetLevel(Level newLevel)
    {
        if (_activeLevel != null)
        {
            _activeLevel.gameObject.SetActive(false);
            Destroy(_activeLevel.gameObject);
            _activeLevel = null;
        }

        var levelPrefab = Instantiate(newLevel.LevelPrefab);
        levelPrefab.Init();
        _activeLevel = levelPrefab;
        _isAnimActive = false;
    }

    public void TestForLevelEnd()
    {
        if (_activeLevel.IsCompleteDraw())
        {
            _isAnimActive = true;
            _activeLevel.StartAnim();
            StartCoroutine(MarkerTestDelay(_activeLevel.GetMaxTime()));
        }
    }

    IEnumerator MarkerTestDelay(float time)
    {
        yield return new WaitForSeconds(time + 0.5f);
        TestForLevelWin();
    }

    public void TestForLevelWin()
    {
        if (!_isAnimActive)
        {
            Debug.Log("<i> Time end, but level not complete");
            return;
        }

        if (_activeLevel.IsFinal())
        {
            Debug.Log("<!> Level Complete, You WIN");
        }
        else
        {
            Debug.LogError("Time end, but level not complete");
        }
    }

    public void StopAnim(Vector3 endPos)
    {
        Debug.Log($"<!> You Lose! End pos: {endPos}");
        StopAnim();
    }

    public void StopAnim()
    {
        if (_activeLevel.IsCompleteDraw())
        {
            _activeLevel.StopAnim();
        }
        _isAnimActive = false;
    }

    public void RestartLevel()
    {
        _activeLevel.ResetLevel();
        _isAnimActive = false;
    }
}
