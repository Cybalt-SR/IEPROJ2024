using Assets.Scripts.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject Enem;
    // private NavMeshSurface NSurface;
    [SerializeField] private int EnemCount;
    [SerializeField] private int limit = 20;
    [SerializeField] private int spawned_alr = 0;

    [SerializeField] private float spawn_interval;
    
    Collider[] overlapresults = new Collider[1];

    void Start()
    {
        InvokeRepeating(nameof(createWave), 0, spawn_interval);
    }

    private void createWave()
    {
        var anchorlist = FindObjectsByType<SpawnerAnchor>(FindObjectsSortMode.None);

        for (int i = 0; i < EnemCount; i++)
        {
            if (spawned_alr >= limit)
                return;

            foreach (var anchor in anchorlist)
            {
                if (Physics.OverlapSphereNonAlloc(anchor.transform.position, 0.1f, overlapresults) > 0)
                    continue;

                int a_i = i % anchorlist.Length;

                Instantiate(Enem, anchor.transform.position, Quaternion.Euler(0, -45, 0));

                spawned_alr++;
                break;
            }
        }
    }
}

