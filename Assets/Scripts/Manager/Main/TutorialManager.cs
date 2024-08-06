using Assets.Scripts.Controller;
using Assets.Scripts.Library;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class TutorialManager : MonoBehaviour, IConsistentDataHolder<TutorialProgress>
{
    [SerializeField] private Transform TutorialPopUp;
    [SerializeField] private TextMeshPro TutorialPopUp_text;
    [SerializeField] private float prompt_duration;
    [SerializeField] private float animation_speed = 20;

    [SerializeField] private TutorialProgress mData = new();

    private Transform current_reference = null;
    private float time_prompt_active = 1000;

    private void Awake()
    {
        IConsistentDataHolder<TutorialProgress>.Data = mData;
    }

    private void Start()
    {
        ActionBroadcaster.WaitFor(TutorialDictionary.Instance.prompts[mData.prompt_index].trigger_broadcast);

        ActionBroadcaster.Subscribe((actionname, reference) =>
        {
            if (mData.prompt_index >= TutorialDictionary.Instance.prompts.Count)
                return;

            var next_prompt = TutorialDictionary.Instance.prompts[mData.prompt_index];

            if (actionname == next_prompt.trigger_broadcast)
            {
                time_prompt_active = 0;

                current_reference = reference;
                TutorialPopUp_text.text = next_prompt.message;

                mData.prompt_index++;

                if (mData.prompt_index >= TutorialDictionary.Instance.prompts.Count)
                    return;

                ActionBroadcaster.WaitFor(TutorialDictionary.Instance.prompts[mData.prompt_index].trigger_broadcast);
            }
        });
    }

    private void Update()
    {
        time_prompt_active += Time.deltaTime;

        if (current_reference != null)
            TutorialPopUp.position = current_reference.position;
        else
            TutorialPopUp.position = PlayerController.GetFirst().transform.position;

        if (time_prompt_active > prompt_duration)
            TutorialPopUp.localScale = Vector3.Lerp(TutorialPopUp.localScale, Vector3.zero, Time.deltaTime * animation_speed);
        else
            TutorialPopUp.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Clamp01(time_prompt_active * animation_speed));
    }
}
