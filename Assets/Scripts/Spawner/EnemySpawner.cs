using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject Enem;
     [Range(0,10)] private int EnemyNumbers=0;
    // Start is called before the first frame update
    void Awake()
    {
       this.Enem = null;
    }
     void Start()
    {
        
    }
    // Update is called once per frame

}
