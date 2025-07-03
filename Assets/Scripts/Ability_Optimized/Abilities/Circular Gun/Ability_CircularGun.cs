using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilityOP;
using Assets.Scripts.Controller.Attachments;
using System.Linq;
using NUnit.Framework;

[CreateAssetMenu(fileName = "Ability_CircularGun_data", menuName = "Ability Optimized/Abilities/Circular Gun/Ability", order = 2)]
public class Ability_CircularGun_data : AbilityData
{
    [Header("Ability Stats")]
    public int max_spawnable_toys;

    [Header("Assets")]
    public GameObject ballerina;
    public GameObject soldier;
    public GameObject jack_in_a_box;

}

public class Ability_CircularGun : Ability
{

    private Transform m_toy_holder;

    protected override IEnumerator Active()
    {

        var ability_data = AbilityData as Ability_CircularGun_data;

        var near = Owner.GetComponentInChildren<TriggerSphere>().Within;
        near = near.Where(c => c.GetComponent<ToyPickup>() != null).ToList();

        int count = near.Count;

        if (count == 0)
        {
            Debug.Log("Nothing to Awaken");
            yield break;
        }
            
        var indices = Enumerable.Range(0, count).ToList();

        if(count > ability_data.max_spawnable_toys)
        {
            List<int> rand_indices = new();
            for(int i = 0; i < ability_data.max_spawnable_toys; i++)
            {
                int index = UnityEngine.Random.Range(0, indices.Count);
                rand_indices.Add(indices[index]);
                indices.RemoveAt(index);  
            }
            indices = rand_indices;
        }

        List<GameObject> spawnables = new() { ability_data.ballerina, ability_data.soldier, ability_data.jack_in_a_box };

        foreach (int i in indices)
        {
            var pickup_pos= near[i].transform.position;
            int rand_index = UnityEngine.Random.Range(0, spawnables.Count);

            var spawned = GameObject.Instantiate(spawnables[rand_index]);
            spawned.transform.position = pickup_pos;

            near[i].transform.parent = m_toy_holder;
            near[i].gameObject.SetActive(false);
     
        }
      

        yield return null; 
    }

    protected override void OnOwnerSetting(GameObject owner)
    {
        base.OnOwnerSetting(owner);

        var ability_data = m_ability_data as Shot_Circular_Gun_data;

        //Look for Toy Holder Transform under Owner, if it's not there, create it

        if (!Owner.transform.Find("Toy Holder"))
        {
            GameObject toy_holder = new GameObject("Toy Holder");
            toy_holder.transform.parent = Owner.transform;
            m_ownable_assets.Add(toy_holder.transform);
        }

        m_toy_holder = Owner.transform.Find("Toy Holder");

    }

}
