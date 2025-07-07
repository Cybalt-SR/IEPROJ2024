using Assets.Scripts.Library;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaugeManager : MonoSingleton<GaugeManager>
{
    private GameObject current_gauge;

    public static void DisplayGauge(GameObject prefab)
    {
        RemoveCurrentGauge();
        Instance.current_gauge = GameObject.Instantiate(prefab, Instance.transform);
    }

    public static void RemoveCurrentGauge()
    {
        if(Instance.current_gauge)
            Destroy(Instance.current_gauge);
    }
}
