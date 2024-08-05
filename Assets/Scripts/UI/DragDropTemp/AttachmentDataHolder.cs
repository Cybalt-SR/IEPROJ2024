using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Data.Pickup;
using UnityEngine.UI;
using TMPro;

public class AttachmentDataHolder : MonoBehaviour
{

    private int index = -1;

    public int Index { get { return index; } set { index = value; } }

    private Attachment _data = null;
    public Attachment Data { get { return _data; } set { _data = value; } }


    //assumes that the element is an Attachment Prefab, optimize as necessary
    public void injectToUI()
    {

        T FindComponentInObject<T>(string name)
        {
            GameObject obj = transform.Find(name).gameObject;
            return obj.GetComponent<T>();
        }

        if (_data == null)
            return;

        var attachment_icon = FindComponentInObject<Image>("Image");
        attachment_icon.sprite = _data.attachment_icon;

        var attachment_name = FindComponentInObject<TextMeshProUGUI>("Title");
        attachment_name.text = _data.attachment_name;

        var attachment_description = FindComponentInObject<TextMeshProUGUI>("Description");
        attachment_description.text = _data.attachment_description;

        var attachment_type = FindComponentInObject<TextMeshProUGUI>("AttachmentType");
        attachment_type.text = _data.part.ToString();

        
    }

}
