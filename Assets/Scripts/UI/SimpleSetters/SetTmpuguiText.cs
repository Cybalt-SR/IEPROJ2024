using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SetTmpuguiText : MonoBehaviour
{
    private TextMeshProUGUI mTextMeshProUGUI;

    private void Awake()
    {
        mTextMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        mTextMeshProUGUI.text = text;
    }
}
