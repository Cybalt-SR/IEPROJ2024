using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilityOP;



[CreateAssetMenu(fileName = "Boss_Swarm_ToyMaker", menuName = "Ability Optimized/Abilities/Boss Abilities/Toy Maker/Swarm", order = 1)]
public class Boss_Swarm_Toymaker_data : AbilityData
{

    [Header("Spawning")]
    public int minimum_spawn_count;
    public int maximum_spawn;
    public int waves;
    public float spawn_interval;

    [Header("Assets")]
    public List<GameObject> spawnables;

    [Header("Positioning")]
    public float offset;
}
