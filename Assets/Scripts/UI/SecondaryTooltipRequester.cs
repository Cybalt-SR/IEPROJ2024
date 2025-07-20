using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SecondaryTooltipRequester : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private string tooltip_type;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SecondaryTooltipHandler.RequestTooltip(GetComponent<RectTransform>(), tooltip_type);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SecondaryTooltipHandler.DisableTooltip();
    }
}
