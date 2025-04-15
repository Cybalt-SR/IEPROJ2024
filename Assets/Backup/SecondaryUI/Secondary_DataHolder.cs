using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using gab_roadcasting;
using UnityEngine.EventSystems;

public class Secondary_DataHolder : MonoBehaviour, IPointerDownHandler
{

    private Outline outline;

    [Header("Outline Config")]
    [SerializeField] private float outline_thickness = 5f;


    [SerializeField] private Secondary secondaryData;
    public Secondary SecondaryData { get { return secondaryData; } }


    private void Awake()
    {
        outline= GetComponent<Outline>();
        outline.effectDistance = Vector2.one * outline_thickness;
        outline.enabled = false;
    }

    public void setSelected(bool value)
    {
       outline.enabled = value;
    }

    public void injectToUI()
    {

        T FindComponentInObject<T>(string name)
        {
            GameObject obj = transform.Find(name).gameObject;
            return obj.GetComponent<T>();
        }

        if (secondaryData == null)
            return;

        var attachment_icon = FindComponentInObject<Image>("Image");
        attachment_icon.sprite = secondaryData.SecondaryIcon.sprite;

        var attachment_name = FindComponentInObject<TextMeshProUGUI>("Title");
        attachment_name.text = secondaryData.SecondaryName;

    }

   
    public void OnPointerDown(PointerEventData eventData)
    {
        var p = new Dictionary<string, object>();
        p.Add("SecondaryData", secondaryData);
        EventBroadcasting.InvokeEvent(EventNames.UI_EVENTS.ON_SECONDARY_CLICKED, p);
        
    }
}
