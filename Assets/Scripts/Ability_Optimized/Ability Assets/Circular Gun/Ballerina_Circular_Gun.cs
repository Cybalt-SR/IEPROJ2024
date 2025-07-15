using Assets.Scripts.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballerina_Circular_Gun : MonoBehaviour
{

    private Dictionary<EnemyController, GameObject> trapped_enemies = new();
    [SerializeField] private Transform center;
    [SerializeField] private float rotation_speed = 0.3f;

    [SerializeField] private GameObject charmed_particle;


    public float lifetime = 8f;
    private float timer = 0;

    private void OnEnable()
    {
        timer = 0;
    }

    private void Update()
    {
        center.Rotate(0, Time.deltaTime * rotation_speed, 0);
        timer += Time.deltaTime;
        if (timer >= lifetime)
            center.gameObject.SetActive(false); 
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BossController>() != null)
            return;

        var controller = other.GetComponent<EnemyController>();
        if (controller)
        {
            trapped_enemies.Add(controller, Instantiate(charmed_particle));
            controller.enabled = false;
            other.transform.LookAt(center.position);
            trapped_enemies[controller].transform.parent = controller.transform;
            trapped_enemies[controller].transform.localPosition = Vector3.zero;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var controller = other.GetComponent<EnemyController>();
        if (controller)
        {
            Destroy(trapped_enemies[controller]);
            controller.enabled = true;
            trapped_enemies.Remove(controller);
        }
    }

    void EndAllCharm()
    {
        foreach (var e in trapped_enemies)
        {
            if (e.Key)
            {
                e.Key.enabled = true;
            }
            Destroy(e.Value);

        }
        trapped_enemies.Clear();
    }

    private void OnDisable()
    {
        EndAllCharm();
    }

    private void OnDestroy()
    {
        EndAllCharm();
    }

}
