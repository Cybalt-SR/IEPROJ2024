using Assets.Scripts.Data.Pickup;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneDataHolder : MonoBehaviour
{
    private Attachment _data = null;
    public Attachment Data { get { return _data; } set { _data = value; } }
}
