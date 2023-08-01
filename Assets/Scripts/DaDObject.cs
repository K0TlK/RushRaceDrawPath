using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Drag and drop object on scene
/// </summary>
interface DaDObject
{
    public bool isMovable();
    public void Select();
    public void Deselect();
    public void SetPos(Vector3 deltaPos);
}
