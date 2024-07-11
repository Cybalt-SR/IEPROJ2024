using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropUI : MonoBehaviour
{
    [SerializeField] private RectTransform clone;

    public static RectTransform Clone = null;


    //should listen to attachment obtained, should have pool to manage attachment containers

    private void Awake()
    {

        if (Clone == null)
            Clone = clone;


        clone.GetComponent<CanvasGroup>().blocksRaycasts = false;
        foreach(Transform t in transform)
        {

            //messy af will fix when other dependencies are done

            DragDrop d = t.gameObject.GetComponent<DragDrop>();

            if(d == null)
                d = t.gameObject.AddComponent<DragDrop>();

            d.Clone = clone;
        }
    }
}
