using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public int size;
        public int index;
        public GameObject EFab;
    }

    public GameObject Enem;
    public List<Pool> pools;
     [Range(0,10)] public int EnemyNumbers=0;
    public Dictionary<int, Queue<GameObject>> EnemyPool;
    
     void Start()
    {
        EnemyPool = new Dictionary<int, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();


            for(int i = 0; i<pool.size;i++)
            {

            }
        }
    }
    // Update is called once per frame

}
