using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthObject))]
public class EnableAndUnparentOnDie : MonoBehaviour
{

    private HealthObject mHealthObject;

    [SerializeField] private List<GameObject> toDetach;

    private void Awake()
    {
        mHealthObject = GetComponent<HealthObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mHealthObject.SubscribeOnDie(projectile =>
        {
            foreach (var item in toDetach)
            {
                item.SetActive(true);
                item.transform.parent = null;
            }   
        });
    }
}
