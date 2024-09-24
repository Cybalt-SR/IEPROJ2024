using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Gameplay.Manager;
using Abilities;
using gab_roadcasting;

public class SecondaryManager : Manager_Base<SecondaryManager>
{

    [SerializeField] private Secondary currentlyEquippedSecondary = null;

    private Ability shotEffect;
    private Ability secondaryAbility;

    public bool hasEquipped { get { return currentlyEquippedSecondary != null; } }


    protected override void Awake()
    {
        base.Awake();
        EventBroadcasting.AddListener(EventNames.SECONDARY_EVENTS.ON_SECONDARY_EQUIP, (Dictionary<string, object> p) => Equip(p["Secondary"] as Secondary));
    }

    public void Equip(Secondary secondary)
    {
        if (currentlyEquippedSecondary != null)
            Unequip();

        currentlyEquippedSecondary = secondary;

        shotEffect = AbilityManager.Instance.RequestAbility(secondary.ShotEffect.AbilityData.AbilityID, gameObject);
        secondaryAbility = AbilityManager.Instance.RequestAbility(secondary.SecondaryAbility.AbilityData.AbilityID, gameObject);
    }


    private void Update()
    {
        if (hasEquipped == false)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            shotEffect.Activate();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            secondaryAbility.Activate();
        }
    }


    private void Unequip()
    {
        if (hasEquipped)
        {
            AbilityManager.Instance.ReleaseAbility(shotEffect);
            AbilityManager.Instance.ReleaseAbility(secondaryAbility);
        }
    }


}
