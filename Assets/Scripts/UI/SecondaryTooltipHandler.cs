using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class SecondaryTooltipHandler : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI tooltip_text;

    private void Update()
    {
        var rt = transform as RectTransform;
        rt.anchoredPosition = Mouse.current.position.value;
    }

    public void RequestTooltip(string message)
    {
        gameObject.SetActive(true);
        tooltip_text.text = message;
    }

    public void DisableTooltip()
    {
        gameObject.SetActive(false);
    }

}
