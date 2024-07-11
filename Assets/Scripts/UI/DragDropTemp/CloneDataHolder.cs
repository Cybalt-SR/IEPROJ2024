using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneDataHolder : MonoBehaviour
{

    [SerializeField] private Image _image;
    public Image Image { get { return _image; } set { _image = value; } }

    private GameObject _data = null;
    public GameObject Data { get { return _data; } set { _data = value; } }
}
