using Assets.Scripts.Controller;
using Assets.Scripts.Library.Modules.TrackedVariable;
using Assets.Scripts.Manager.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class HUD : MonoBehaviour, IPlayerSpecificUi
{
    PlayerController p;
    OverloadHealthObject hp;

    string IPlayerSpecificUi.PlayerAssigned { get; set; }

    float cache_hp;
    (int count, int max) cache_bullet;
    float cache_reloadprogress;

    TrackedVariable<float> hp_tracker;
    TrackedVariable<(int count, int max)> bulletcount_tracker;
    TrackedVariable<float> reloadprogress_tracker;

    [SerializeField] private UnityEvent<float> onHealthRatioChanged;
    [SerializeField] private UnityEvent<(int count, int max)> onBulletCountChanged;
    [SerializeField] private UnityEvent<float> onReloadProgressChanged;

    private void Start()
    {
        p = PlayerManager.GetByName((this as IPlayerSpecificUi).PlayerAssigned);

        hp = p.GetComponent<OverloadHealthObject>();
        hp_tracker = new TrackedVariable<float>(onHealthRatioChanged.Invoke, () => (hp.MaxHeat - hp.Heat) / hp.MaxHeat);
        bulletcount_tracker = new TrackedVariable<(int count, int max)>(onBulletCountChanged.Invoke, () => (p.Shots_before_reload, p.Gun.clip_size));
        reloadprogress_tracker = new TrackedVariable<float>(onReloadProgressChanged.Invoke, () =>
        {
            if (p.Reloading)
                return p.Time_last_reload / p.Gun.reload_time;
            else
                return (float)p.Shots_before_reload / p.Gun.clip_size;
        });
    }

    private void Update()
    {
        hp_tracker.CheckChange();
        bulletcount_tracker.CheckChange();
        reloadprogress_tracker.CheckChange();
    }
}
