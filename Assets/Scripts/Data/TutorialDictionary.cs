using Assets.Scripts.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Dictionary/Tutorial")]
public class TutorialDictionary : SingletonResource<TutorialDictionary>
{
    [Serializable]
    public class TutorialPrompt
    {
        public string trigger_broadcast;
        [TextArea] public string message;
    }

    public List<TutorialPrompt> prompts;
}
