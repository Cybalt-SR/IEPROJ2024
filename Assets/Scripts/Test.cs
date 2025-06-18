using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Test : MonoBehaviour
{
    float time = 0;
    float max_duration = 8;
    bool displayed = false;

    float deltaTime = 0; 

    //Frame Agnostic Wait Until
    private async Task WaitUntil(Func<bool> predicate, Func<bool> fallback = null)
    {
        while (!predicate())
        {
            if (fallback != null && fallback())
                break;
            
            await Task.Delay(1000);
        }
    }

    private async Task WaitTester()
    {
        Debug.Log("Wait Started");
        await WaitUntil(() => time >= 8);
        Debug.Log("I've been Waiting for " + time);
    }

    private void Start()
    {
        Task.Run(async () => await WaitTester());
    }

    private void Update()
    {
        time += Time.deltaTime;

        if(time >= 8 && !displayed)
        {
            Debug.Log(time);
            displayed = true;
        }
    }

}
