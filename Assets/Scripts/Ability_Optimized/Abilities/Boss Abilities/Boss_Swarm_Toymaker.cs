using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilityOP;
using Assets.Scripts.Controller;
using UnityEngine.AI;

public class Boss_Swarm_Toymaker : Ability
{
    private List<GameObject> enemies_spawned = new();

    private List<GameObject> spawnable_templates = new();

    protected override void OnOwnerSetting(GameObject owner)
    {
        base.OnOwnerSetting(owner);
        var ability_data = m_ability_data as Boss_Swarm_Toymaker_data;
        var spawnables = ability_data.spawnables;

        foreach (var spawnable in spawnables)
        {
            var spawned_unit = GameObject.Instantiate(spawnable);
            spawned_unit.SetActive(false);
            spawned_unit.GetComponent<EnemyController>().enabled = false;
            spawned_unit.GetComponent<NavmeshPhysicsAgent>().enabled = false;
            spawned_unit.GetComponent<NavMeshAgent>().enabled = false;
            spawnable_templates.Add(spawned_unit);
        }
    }

    IEnumerator Delay(float delay, System.Action callback)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }

    protected override IEnumerator Active()
    {
  
        var ability_data = m_ability_data as Boss_Swarm_Toymaker_data;

        int to_spawn =  UnityEngine.Random.Range(ability_data.minimum_spawn_count, ability_data.maximum_spawn);

        for (int i = 0; i < to_spawn; i++)
        {
            int spawnable_index = UnityEngine.Random.Range(0, spawnable_templates.Count);
            var spawned_unit = GameObject.Instantiate(spawnable_templates[spawnable_index]);
            spawned_unit.SetActive(true);

            enemies_spawned.Add(spawned_unit);

            var owner_pos = Owner.transform.position;
            var pos = new Vector3();
            pos.x = Owner.transform.position.x + UnityEngine.Random.Range(-ability_data.offset, ability_data.offset);
            pos.y = Owner.transform.position.y;
            pos.z = Owner.transform.position.z + UnityEngine.Random.Range(-ability_data.offset, ability_data.offset);

            spawned_unit.transform.position = pos;

            spawned_unit.GetComponent<MonoBehaviour>().StartCoroutine(Delay(1.5f, () => {
                spawned_unit.GetComponent<EnemyController>().enabled = true;
                spawned_unit.GetComponent<NavmeshPhysicsAgent>().enabled = true;
                spawned_unit.GetComponent<NavMeshAgent>().enabled = true;
            }));

        }

        yield return null;


    }


    
}
