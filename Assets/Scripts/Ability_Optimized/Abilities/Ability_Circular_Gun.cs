using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Controller.Attachments;
using System.Linq;
using AbilityOP;

public class Ability_Circular_Gun : Ability
{

    private Transform m_toy_holder;
    private Dictionary<string, List<GameObject>> spawnables;

    protected override IEnumerator Active()
    {

        var ability_data = AbilityData as Ability_Circular_Gun_data;

        var near = Owner.GetComponentInChildren<TriggerSphere>().Within;
        near = near.Where(c =>  c && c.activeSelf && c.GetComponent<ToyPickup>() != null ).ToList();

        int count = near.Count;

        if (count == 0)
        {
            Debug.Log("Nothing to Awaken");
            yield break;
        }

        var indices = Enumerable.Range(0, count).ToList();

        if (count > ability_data.max_spawnable_toys)
        {
            List<int> rand_indices = new();
            for (int i = 0; i < ability_data.max_spawnable_toys; i++)
            {
                int index = UnityEngine.Random.Range(0, indices.Count);
                rand_indices.Add(indices[index]);
                indices.RemoveAt(index);
            }
            indices = rand_indices;
        }

        foreach (int i in indices)
        {
            var pickup_pos = near[i].transform.position;
            int rand_index = UnityEngine.Random.Range(0, 3);

            var spawned = RequestToy(rand_index);
            spawned.transform.position = pickup_pos;



            near[i].transform.parent = m_toy_holder;
            near[i].gameObject.SetActive(false);

        }


        yield return null;
    }

    protected override void OnOwnerSetting(GameObject owner)
    {
        base.OnOwnerSetting(owner);

        var ability_data = m_ability_data as Ability_Circular_Gun_data;

        //Look for Toy Holder Transform under Owner, if it's not there, create it

        if (!Owner.transform.Find("Toy Holder"))
        {
            GameObject toy_holder = new GameObject("Toy Holder");
            toy_holder.transform.parent = Owner.transform;
            m_ownable_assets.Add(toy_holder.transform);
        }

        m_toy_holder = Owner.transform.Find("Toy Holder");
     

    }

    protected override void OnOwnerRemoving(GameObject owner)
    {
        base.OnOwnerRemoving(owner);

        foreach(var type in spawnables)
        {
            foreach(var sentry in type.Value)     
               GameObject.Destroy(sentry.gameObject);
        }
    }

    private GameObject RequestToy(int index)
    {
        List<string> names = new() { "Ballerina", "Soldier", "Jack In A Box" };
        var ability_data = m_ability_data as Ability_Circular_Gun_data;

        if (spawnables == null)
        {
            spawnables = new();
            foreach(var n in names)
                spawnables.Add(n, new());   
        }

        index = Mathf.Clamp(index, 0, names.Count - 1);
        List<GameObject> pool = spawnables[names[index]];
        List<GameObject> active_poolables = pool.Where(p => p.activeSelf).ToList(); 

        if (pool.Count > 0)
            return pool[0];

        GameObject sentry = null;
        switch (names[index])
        {
            case "Ballerina":
                sentry = GameObject.Instantiate(ability_data.ballerina);
                var ballerina = sentry.GetComponentInChildren<Ballerina_Circular_Gun>();
                ballerina.lifetime = ability_data.ballerina_lifetime;
                break;

            case "Soldier":
                sentry = GameObject.Instantiate(ability_data.soldier);
                var soldier = sentry.GetComponentInChildren<Nutcracker_Circular_Gun>();
                soldier.lifetime = ability_data.soldier_lifetime;
                soldier.shot_damage = ability_data.soldier_shot_damage;
                soldier.shots_per_second = ability_data.soldier_shots_per_second;
                break;

            case "Jack In A Box":
                sentry = GameObject.Instantiate(ability_data.jack_in_a_box);
                var jack = sentry.GetComponentInChildren<Jack_Circular_Gun>();
                jack.detonation_delay = ability_data.jack_detonation_delay;
                jack.detonation_trigger_distance = ability_data.jack_detonation_trigger_distance;
                jack.knockback_force = ability_data.jack_knockback_force;
                jack.explosion_damage = ability_data.jack_explosion_damage;
                break;
        }

        if(sentry)
            pool.Add(sentry);

        return sentry;
    }

}