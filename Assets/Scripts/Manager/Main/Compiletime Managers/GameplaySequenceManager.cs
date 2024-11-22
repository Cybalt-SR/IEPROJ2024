using Assets.Scripts.Controller;
using Assets.Scripts.Data.Progression;
using Assets.Scripts.Library;
using Assets.Scripts.Library.ActionBroadcaster;
using External.Dialogue;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplaySequenceManager : MonoBehaviour, IConsistentDataHolder<GameplayProgress>
{
    [SerializeField] private Transform TutorialPopUp;

    [SerializeField] private GameplayProgress mData = new();

    private Transform current_reference = null;
    private bool dismissed = false;

    private bool currently_displaying_tutorial = false;

    private void Awake()
    {
        IConsistentDataHolder<GameplayProgress>.Data = mData;
    }

    public void DismissPrompt()
    {
        dismissed = true;
        current_reference = null;
    }

    private void Start()
    {
        for (int i = 0; i < GameplaySequence.Instance.Count; i++)
        {
            var item = GameplaySequence.Instance.GetKey(i);
            var cur_i = i;

            ActionWaiter.RegisterWaiter(item.trigger_event, (Transform reference) =>
            {
                if (cur_i != mData.prompt_index)
                    return false;

                if (ActionRequestManager.Request(item.request))
                {
                    mData.prompt_index++;
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }
    }
}
