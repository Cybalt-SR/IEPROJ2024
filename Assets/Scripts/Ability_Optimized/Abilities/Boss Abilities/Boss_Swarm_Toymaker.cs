using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilityOP;

public class Boss_Swarm_Toymaker : Ability
{
    private List<GameObject> enemies_spawned = new();

    protected override IEnumerator Active()
    {
        Debug.Log("Activate");
        var ability_data = m_ability_data as Boss_Swarm_Toymaker_data;
        int wave_count = 0;
        var spawnables = ability_data.spawnables;

        while(wave_count < ability_data.waves)
        {
            int to_spawn = ability_data.minimum_spawn_count + UnityEngine.Random.Range(0, ability_data.maximum_spawn);

            for(int i =0; i < to_spawn; i++)
            {
                int spawnable_index = UnityEngine.Random.Range(0, spawnables.Count - 1);
                var spawned_unit = GameObject.Instantiate(spawnables[spawnable_index]);
                enemies_spawned.Add(spawned_unit);
                var owner_pos = Owner.transform.position;

                var pos = new Vector3();
                pos.x = Owner.transform.position.x + UnityEngine.Random.Range(-ability_data.offset, ability_data.offset);
                pos.y = Owner.transform.position.y;
                pos.z = Owner.transform.position.z + UnityEngine.Random.Range(-ability_data.offset, ability_data.offset);

                spawned_unit.transform.position = pos;  
            }


            yield return new WaitForSeconds(ability_data.spawn_interval);
        }


    }
}
