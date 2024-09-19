using Assets.Scripts.Controller;
using Assets.Scripts.Library;
using External.Dialogue;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour, IConsistentDataHolder<TutorialProgress>
{
    [SerializeField] private Transform TutorialPopUp;
    [SerializeField] private float animation_speed = 20;

    [SerializeField] private TutorialProgress mData = new();

    private Transform current_reference = null;
    private bool dismissed = false;

    private bool currently_displaying_tutorial = false;

    private void Awake()
    {
        IConsistentDataHolder<TutorialProgress>.Data = mData;
    }

    public void DismissPrompt()
    {
        dismissed = true;
        current_reference = null;
    }

    private void Start()
    {
        for (int i = 0; i < TutorialDictionary.Instance.GetCount(); i++)
        {
            var item = TutorialDictionary.Instance.GetKey(i);
            var cur_i = i;

            ActionBroadcaster.WaitFor(item, (Transform reference) =>
            {
                if (cur_i != mData.prompt_index)
                    return false;

                if (reference == null)
                    reference = PlayerController.GetFirst().transform;

                current_reference = reference;
                var loaded = DialogueController.LoadMessage(new Message()
                {
                    text = TutorialDictionary.Instance.GetCompiledMessage(cur_i)
                });

                if (loaded)
                    mData.prompt_index++;

                return loaded;
            });
        }
    }

    private void Update()
    {
        if (dismissed == false)
        {
            if (current_reference != null)
                TutorialPopUp.position = current_reference.position;

            TutorialPopUp.localScale = Vector3.Lerp(TutorialPopUp.localScale, Vector3.one, Time.deltaTime * animation_speed);
        }
        else
            TutorialPopUp.localScale = Vector3.Lerp(TutorialPopUp.localScale, Vector3.zero, Time.deltaTime * animation_speed);
    }
}
