using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClearCheck : MonoBehaviour
{
    public GameObject NextLevel;
    public int EnemiesToKill;
    private int enemiesKilled = 0;
    // Start is called before the first frame update
    void Start()
    {
        NextLevel.SetActive(false);
    }


    public void AddKill()
    {
        enemiesKilled++;
        if(enemiesKilled >= EnemiesToKill)
        {
            NextLevel.SetActive(true);
        }
    }
}
