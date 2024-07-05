using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Controller;
using Assets.Scripts.Input;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MagazineUpdate : MonoBehaviour
{

    [SerializeField] private GameObject bulletInfo;
    [SerializeField] private GameObject reloadInfo;
    private PlayerController player;


    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        reloadInfo.SetActive(false);
        bulletInfo.GetComponent<Text>().text = player.Shots_before_reload.ToString() + "/" + player.Gun.clip_size.ToString();
    }

    private void Update()
    {

        bulletInfo.GetComponent<Text>().text = player.Shots_before_reload.ToString() + "/" + player.Gun.clip_size.ToString();
       
            if (player.Reloading)
            {
                reloadInfo.SetActive(true);
                reloadInfo.GetComponent<Text>().text = Math.Round(((player.Gun.reload_time - player.Time_last_reload)/player.Gun.reload_time*100),0).ToString() + "%";
            }
            else reloadInfo.SetActive(false);
       


    }
}
