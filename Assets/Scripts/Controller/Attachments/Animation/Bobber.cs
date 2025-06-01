using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    [SerializeField] float bobstrength = 1.0f;
    [SerializeField] float bobspeed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        var curlocalpos = this.transform.localPosition;

        curlocalpos.y = Mathf.Sin(Time.time * bobspeed) * bobstrength;

        this.transform.localPosition = curlocalpos;
    }
}
