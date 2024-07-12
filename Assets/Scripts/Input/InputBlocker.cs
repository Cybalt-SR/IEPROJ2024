using Assets.Scripts.Input;
using Assets.Scripts.Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBlocker : MonoBehaviour
{
    [SerializeReference] public MonoBehaviour inputReceiver;

    private void OnEnable()
    {
        InputController.BlockerEnabled(this);
    }

    private void OnDisable()
    {
        InputController.BlockerDisabled(this);
    }
}
