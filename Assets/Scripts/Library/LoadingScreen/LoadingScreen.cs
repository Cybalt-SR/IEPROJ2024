using Assets.Scripts.Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor.Sprites;

public class LoadingScreen : MonoSingleton<LoadingScreen>
{
    Animator blocker_animator;

    static void LoadScreen(Action do_when_loading)
    {
    }

    IEnumerator Load(Action do_when_loading)
    {


        yield return null;
    }
}
