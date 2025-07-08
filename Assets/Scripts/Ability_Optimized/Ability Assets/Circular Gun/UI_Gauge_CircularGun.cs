using Assets.Scripts.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Gauge_CircularGun: MonoBehaviour
{
    [SerializeField] private GameObject ammo_ui_prefab;

    private void Update()
    {
        var player_transform = PlayerController.GetFirst().transform;
        var toy_holder = player_transform.Find("Toy Holder");

        if (!toy_holder)
            return;

        int ammo_count = toy_holder.childCount;
        int container_count = transform.childCount;

        if (container_count < ammo_count)
        {
            for (int i = 0; i < ammo_count - container_count; i++)
            {
                var go = Instantiate(ammo_ui_prefab, transform);
            }
        }

        foreach(Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }

        for(int i = 0; i< ammo_count; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

    }
}
