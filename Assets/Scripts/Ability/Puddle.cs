using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : ProximityChecker
{
    [SerializeField] private float damageOverTime = 1;
    [SerializeField] private float damageInterval = 2.5f;

    [SerializeField] private float puddleLifetime = 6f;

    private float timer = 0f;

    UnitController Creator { get; set; }

    private void Start()
    {
        IEnumerator lifetime()
        {
            yield return new WaitForSeconds(puddleLifetime);
            Destroy(gameObject);
        }
        StartCoroutine(lifetime());

        timer = damageInterval;
    }

    private void Update()
    {

        timer -= Time.deltaTime;

        if (timer > 0f)
            return;

        foreach(var enemy in collisionList)
        {
            var healthObject = enemy.GetComponent<HealthObject>();
            healthObject.TakeDamage(damageOverTime, Creator);
        }

        timer = damageInterval;
    }
}
