using Assets.Scripts.Controller;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Gauge_CircularGun: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counter;

    private void Update()
    {
        var player_transform = PlayerController.GetFirst().transform;
        var toy_holder = player_transform.Find("Toy Holder");

        if (toy_holder)
        { 
            counter.text = $"x {1 + toy_holder.childCount}";
        }

    } 
}
