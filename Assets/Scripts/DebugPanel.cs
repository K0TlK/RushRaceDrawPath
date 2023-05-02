using RashLevel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPanel : MonoBehaviour
{
    public void LoadNext()
    {
        LevelLoader.Instance.NextLevel();
    }
    public void LoadPrev()
    {
        LevelLoader.Instance.PrevLevel();
    }
}
