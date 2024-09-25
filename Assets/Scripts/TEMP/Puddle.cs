using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    [SerializeField] private float damageOverTime = 1;
    [SerializeField] private float damageInterval = 2.5f;
    [SerializeField] private List<string> tagWhitelist;
    [SerializeField] private float puddleLifetime = 6f;

    private float timer = 0f;

    private bool willDealDoT = false;


    private void Start()
    {
        //make a pooler for this
        IEnumerator lifetime()
        {
            yield return new WaitForSeconds(puddleLifetime);
            Destroy(gameObject);
        }
        StartCoroutine(lifetime());
    }


    private void OnTriggerStay(Collider other)
    {
        if (willDealDoT && tagWhitelist.Contains(other.tag))
        {
            other.gameObject.GetComponent<HealthObject>().TakeDamage(damageOverTime);
            willDealDoT = false;
        }
                
    }

    private void Update()
    {
        if (willDealDoT)
            return;

        timer += Time.deltaTime;

        if(timer >= damageInterval)
        {
            timer = 0f;
            willDealDoT = true;
        }
    }
}
