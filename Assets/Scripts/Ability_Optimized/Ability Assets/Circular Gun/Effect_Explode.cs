using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Explode : MonoBehaviour
{

    [SerializeField] private Vector3 StartScale;
    [SerializeField] private Vector3 TargetScale;
    [SerializeField] private float ExplosionSpeed;
    [SerializeField] private float DestroyDelay;

    private void OnEnable()
    {
        transform.localScale = StartScale;
    }
    private void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, TargetScale, Time.deltaTime * ExplosionSpeed);
        if(Vector3.Distance(transform.localScale, TargetScale) <= 0.1f)
        {
            IEnumerator DelayedDestroy()
            {
                yield return new WaitForSeconds(DestroyDelay);
                Destroy(gameObject);
            }
            StartCoroutine(DelayedDestroy());
        }
           
    }

}
