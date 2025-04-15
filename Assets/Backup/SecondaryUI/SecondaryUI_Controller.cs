using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gab_roadcasting;

public class SecondaryUI_Controller : GameObjectPoolManager
{

    private Secondary currentlySelected = null;

    protected override void Awake()
    {
        base.Awake();
        EventBroadcasting.AddListener(EventNames.UI_EVENTS.ON_SECONDARY_CLICKED, OnSecondaryClicked);

    }

    private void OnSecondaryClicked(Dictionary<string, object> parameters)
    {

        var key = parameters["SecondaryData"] as Secondary;

        foreach(var secondary in pool)
        {
            var DataHolder = secondary.GetComponent<Secondary_DataHolder>();

            // DataHolder.setSelected(secondary.activeInHierarchy && DataHolder.SecondaryData == key);
         
            if (secondary.activeInHierarchy && DataHolder.SecondaryData == key)
            {
                currentlySelected = key;
                DataHolder.setSelected(true);
            }
            else DataHolder.setSelected(false);
            
        }
    }

    protected override void attachComponents(GameObject go)
    {
        go.AddComponent<Secondary_DataHolder>();
    }

}
