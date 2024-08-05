using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Library;
using Assets.Scripts.Data.Pickup;

public class SlotManager : MonoBehaviour
{


    [SerializeField] public SerializableDictionary<Attachment.Part, AttachmentSlot> slots;

    private void Start()
    {
        foreach (var slot in slots)
            slot.Value.Part = slot.Key;       
    }

    #region tempSingleton

    public static SlotManager instance { get; private set; } = null;


    private void Awake()
    {

        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }


    #endregion

}
