using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{

    public GameObject Enem;
   // private NavMeshSurface NSurface;
    public int EnemCount;
   public NavMeshSurface bnd;

    void Start()
    {
        if (GameObject.Find("Map").GetComponent<NavMeshSurface>() != null)
        {
            bnd = GameObject.Find("Map").GetComponent<NavMeshSurface>();
            this.createWave();
        }
    }

    private void createWave()
    {
    //    NSurface.

        for (int i = 0; i < EnemCount; i++)
        {
            float rx = UnityEngine.Random.Range(bnd.navMeshData.sourceBounds.min.x, bnd.navMeshData.sourceBounds.max.x);
            float rz = UnityEngine.Random.Range(bnd.navMeshData.sourceBounds.min.z, bnd.navMeshData.sourceBounds.max.z);
            Vector3 random=new Vector3(rx,1,rz);
            Instantiate(Enem,random,Quaternion.identity);
        }  
    }
}
    // Update is called once per frame


