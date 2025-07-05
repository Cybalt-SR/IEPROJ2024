using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilityOP;

public class Ability_Slime_Gun: Ability
{
    private Slime_Gun_Hook m_hook;
    private Slime_Gun_Grappler m_grappler;
    private Slime_Gun_Adhesive m_adhesive;

    protected override IEnumerator Active()
    {
        yield return null;
    }

    protected override void OnOwnerSetting(GameObject owner)
    {
        base.OnOwnerSetting(owner);
        
        var ability_data = m_ability_data as Ability_Slime_Gun_data;

        m_hook = Owner.GetComponentInChildren<Slime_Gun_Hook>();
        m_grappler = Owner.GetComponent<Slime_Gun_Grappler>();

        var adhesive_object = GameObject.Instantiate(ability_data.adhesive.gameObject);

    }
}
