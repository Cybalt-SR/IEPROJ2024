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
        ActionBroadcaster.WaitFor(TutorialDictionary.Instance.GetKey(mData.prompt_index), () => currently_displaying_tutorial);

        ActionBroadcaster.Subscribe((actionname, reference) =>
        {
            if (actionname == TutorialDictionary.Instance.GetKey(mData.prompt_index))
            {
                if (reference == null)
                    reference = PlayerController.GetFirst().transform;

                current_reference = reference;
                var loaded = DialogueController.LoadMessage(new Message(){
                    text = TutorialDictionary.Instance.GetCompiledMessage(mData.prompt_index)
                });

                if (loaded)
                {
                    dismissed = false;
                    mData.prompt_index++;

                    if (mData.prompt_index >= TutorialDictionary.Instance.GetCount())
                        return;

                    var index_at_this_point = mData.prompt_index;
                    ActionBroadcaster.WaitFor(TutorialDictionary.Instance.GetKey(mData.prompt_index), () => mData.prompt_index > index_at_this_point);
                }
            }
        });
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
