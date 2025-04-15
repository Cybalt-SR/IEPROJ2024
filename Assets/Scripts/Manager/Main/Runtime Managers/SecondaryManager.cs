using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Assets.Scripts.Gameplay.Manager;
using Abilities;
using gab_roadcasting;
using Assets.Scripts.Controller;

public class SecondaryManager : Manager_Base<SecondaryManager>
{

    [SerializeField] private Secondary currentlyEquippedSecondary = null;

    private Ability shotEffect;
    private Ability secondaryAbility;

    public Secondary CurrentlyEquipped { get { return currentlyEquippedSecondary; } }

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


        GameObject player = PlayerController.GetFirst().gameObject;

        shotEffect = AbilityManager.Instance.RequestAbility(secondary.ShotEffect.AbilityData.AbilityID, player);
        secondaryAbility = AbilityManager.Instance.RequestAbility(secondary.SecondaryAbility.AbilityData.AbilityID, player);
    }


    private void Update()
    {
        if (hasEquipped == false)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            shotEffect.Activate();
        }

        if (Input.GetKeyDown(KeyCode.F))
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
