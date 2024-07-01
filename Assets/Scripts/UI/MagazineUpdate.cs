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
        bulletInfo.GetComponent<Text>().text = player.shots_before_reload.ToString() + "/" + player.mGun.clip_size.ToString();
    }

    private void Update()
    {

        bulletInfo.GetComponent<Text>().text = player.shots_before_reload.ToString() + "/" + player.mGun.clip_size.ToString();
       
            if (player.reloading)
            {
                reloadInfo.SetActive(true);
                reloadInfo.GetComponent<Text>().text = Math.Round(player.mGun.reload_time - player.time_last_reload, 2).ToString();
            }
            else reloadInfo.SetActive(false);
       


    }
}
