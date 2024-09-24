using Assets.Scripts.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Game/Dictionary/Tutorial")]
public class TutorialDictionary : SingletonResource<TutorialDictionary>
{
    [Serializable]
    struct ActionStringText
    {
        public string key;
        public InputActionReference actionReference;
    }
    [SerializeField] private List<ActionStringText> actionStringTexts;

    [Serializable]
    public class TutorialPrompt
    {
        public string trigger_broadcast;
        [TextArea] public string message;
    }

    [SerializeField] private List<TutorialPrompt> prompts;

    public int GetCount()
    {
        return prompts.Count;
    }

    public string GetKey(int index)
    {
        if (index >= prompts.Count)
            return "";

        return prompts[index].trigger_broadcast;
    }

    public string GetCompiledMessage(int index)
    {
        string Bracketize(string text)
        {
            return "[" + text + "]";
        }

        var rawtext = prompts[index].message;

        foreach (var actionString in actionStringTexts)
        {
            rawtext = rawtext.Replace(Bracketize(actionString.key), Bracketize(actionString.actionReference.action.GetBindingDisplayString()));
        }

        return rawtext;
    }
}
