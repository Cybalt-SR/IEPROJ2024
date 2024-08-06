using Assets.Scripts.Library;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GoalText : MonoSingleton<GoalText>
{
    private TextMeshProUGUI mTextMeshProUGUI;

    protected override void Awake()
    {
        base.Awake();

        mTextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public static void SetText(string text)
    {
        if (Instance == null)
            return;

        Instance.mTextMeshProUGUI.text = text;
    }
}
