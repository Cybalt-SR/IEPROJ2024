using Assets.Scripts.Controller;
using Assets.Scripts.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour, IConsistentDataHolder<TutorialProgress>
{
    [SerializeField] private Transform TutorialPopUp;
    [SerializeField] private TextMeshPro TutorialPopUp_text;
    [SerializeField] private float animation_speed = 20;

    [SerializeField] private TutorialProgress mData = new();

    private Transform current_reference = null;
    private bool dismissed = false;

    private void Awake()
    {
        IConsistentDataHolder<TutorialProgress>.Data = mData;
    }

    public void DismissPrompt()
    {
        current_reference = null;

        dismissed = true;
        mData.prompt_index++;

        if (mData.prompt_index >= TutorialDictionary.Instance.GetCount())
            return;

        ActionBroadcaster.WaitFor(TutorialDictionary.Instance.GetKey(mData.prompt_index));
    }

    private void Start()
    {
        ActionBroadcaster.WaitFor(TutorialDictionary.Instance.GetKey(mData.prompt_index));

        ActionBroadcaster.Subscribe((actionname, reference) =>
        {
            if (mData.prompt_index >= TutorialDictionary.Instance.GetCount())
                return;
            
            
            if (actionname == TutorialDictionary.Instance.GetKey(mData.prompt_index))
            {
                if (reference == null)
                    reference = PlayerController.GetFirst().transform;

                current_reference = reference;
                dismissed = false;
                TutorialPopUp_text.text = TutorialDictionary.Instance.GetCompiledMessage(mData.prompt_index);
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
