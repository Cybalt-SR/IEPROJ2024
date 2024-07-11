using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private RectTransform _clone;


    //temp, pass attachment data instead via CloneDataHolder
    [SerializeField] private Image image;


    public RectTransform Clone {get{ return _clone; } set { _clone = value; } }



    public void OnDrag(PointerEventData eventData)
    {
        _clone.position = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _clone.gameObject.SetActive(true);
        _clone.position = GetComponent<RectTransform>().position;
        GetComponent<CanvasGroup>().alpha = 0.5f;


        CloneDataHolder d = _clone.GetComponent<CloneDataHolder>();

        d.Data = gameObject;
        d.Image.sprite = image?.sprite; 
        

        //setAllDragged(transform, true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //setAllDragged(transform, false);
        _clone.position = Vector2.zero;
        _clone.gameObject.SetActive(false);
        GetComponent<CanvasGroup>().alpha = 1f;
    }

    /*
    //reduces the alpha of all components by 50% if true, reverts otherwise
    private void setAllDragged(Transform tf, bool value)
    {

        void setAlpha<T>(T element, float targetAlpha) where T: Graphic
        {
            Color c = element.color;
            c.a = targetAlpha;
            element.color = c;
        };

        void setTransformAlpha(Transform t, float targetAlpha)
        {
            var image = t.GetComponent<Image>();
            var text = t.GetComponent<TextMeshProUGUI>();

            if (image)
                setAlpha<Image>(image, targetAlpha);

            if (text)
                setAlpha<TextMeshProUGUI>(text, targetAlpha);

        }

        float targetAlpha = value ? 0.5f : 1f;

        setTransformAlpha(tf, targetAlpha);

        foreach (Transform t in tf)
        {
            setTransformAlpha(t, targetAlpha);
            setAllDragged(t, value);
        }
    }
    */

   


}
