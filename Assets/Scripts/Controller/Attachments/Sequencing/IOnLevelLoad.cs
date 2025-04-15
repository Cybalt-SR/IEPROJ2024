using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnLevelLoad
{
    void OnLevelExit(GameObject curLevel);
    void OnLevelLoad(GameObject newLevel);
}
