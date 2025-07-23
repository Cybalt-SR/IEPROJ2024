using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowControlsOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ControlsUI controls_screen;

    private void OnEnable()
    {
        controls_screen.DisableControlsScreen();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        controls_screen.InvokeControls(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        controls_screen.DisableControlsScreen();
    }
}
